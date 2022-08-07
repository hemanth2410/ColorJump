using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    bool isDragging = false;

    Vector2 TouchPosition;
    Vector2 PlayerPosition;
    Vector2 DragPosition;

    float RightEnd;
    float LeftEnd;
    float Height;


    public GameObject FX_Jump;
    public GameObject FX_StepDestory;
    public GameObject FX_Dead;

    StepManager stepManager;



    GameManager gameManager;

    bool isStart = false;
    bool isDead = false;

    AudioSource source;
    public AudioClip JumpClip;
    public AudioClip DeadClip;


    float JumpVelocity;

    public float gravity;
    public float maxGravity;
    public float gravityIncrease;

    [SerializeField] bool NegativeColor;
    [SerializeField] ParticleSystem effects;
    Material fxMaterial;
    CinemachineImpulseSource impulseSource;

    void Start()
    {
        

        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        stepManager = GameObject.Find("StepManager").GetComponent<StepManager>();

        RightEnd = GameObject.Find("GameManager").GetComponent<DisplayManager>().RIGHT;
        LeftEnd = GameObject.Find("GameManager").GetComponent<DisplayManager>().LEFT;
        Height = GameObject.Find("GameManager").GetComponent<DisplayManager>().HEIGHT;
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }




    void Update()
    {
        WaitToTouch();
        if (!isStart) return;
        if (isDead) return;

        GetInput();
        MovePlayer();
        AddGravityToPlayer();

        DeadJudgement();
    }


    void WaitToTouch()
    {
        if (!isStart)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!isStart) isStart = true;
                gameManager.StartGame();
            }
        }
    }


    void DeadJudgement()
    {
        if (isDead == false && Camera.main.transform.position.y - transform.position.y > Height / 2)
        {
            isDead = true;
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            GameOver();
        }
    }


    void GameOver()
    {
        GameObject effectObj = Instantiate(FX_Dead, transform.position, Quaternion.identity);
        gameManager.GameOver();
        source.PlayOneShot(DeadClip, 1);
    }

    void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isStart) isStart = true;

            isDragging = true;
            TouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            PlayerPosition = transform.position;

        }
        else if ((Input.GetMouseButtonUp(0)))
        {
            isDragging = false;
        }
    }



    void MovePlayer()
    {
        if (isDragging == true)
        {
            DragPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            transform.position = new Vector3(PlayerPosition.x + (DragPosition.x - TouchPosition.x) * 1.5f, transform.position.y);

            if (transform.position.x < LeftEnd)
                transform.position = new Vector3(LeftEnd, transform.position.y);
            if (transform.position.x > RightEnd)
                transform.position = new Vector3(RightEnd, transform.position.y);
        }
    }




    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Step")
        {
            if (rb.velocity.y <= 0)
            {
                Jump();
                Effect(other);
                ChangeBackgroundColor(other);
                DestroyAndCreateNewStep(other);
                IncreaseGravity();

                gameManager.AddScore(1);

                source.PlayOneShot(JumpClip, 1);
#if UNITY_WEBGL
                WebGLBehaviour.Instance.ResetValue();
#endif
            }
        }

    }



    void Jump()
    {
        JumpVelocity = gravity * 28;
        rb.velocity = new Vector2(0, JumpVelocity);
        impulseSource.GenerateImpulse();
    }

    void Effect(Collider2D step)
    {
        GameObject jumpEffect = Instantiate(FX_Jump, transform.position, Quaternion.identity);
        Destroy(jumpEffect, 1.0f);

        GameObject stepDestroyEffect = Instantiate(FX_StepDestory, step.gameObject.transform.position, Quaternion.identity);
        Destroy(stepDestroyEffect, 0.5f);
    }

    void DestroyAndCreateNewStep(Collider2D step)
    {
        Destroy(step.gameObject);
        stepManager.MakeNewStep();
    }


    void AddGravityToPlayer()
    {
        rb.velocity = new Vector2(0, rb.velocity.y - (gravity * gravity));
    }

    void ChangeBackgroundColor(Collider2D step)
    {
        //Color temp = step.gameObject.GetComponent<SpriteRenderer>().color;
        //Camera.main.backgroundColor = NegativeColor ? new Color(1 - temp.r, 1 - temp.g, 1 - temp.b, 1.0f) : new Color(temp.r - 0.2f, temp.g - 0.2f, temp.b - 0.2f, 1.0f);
        //fxMaterial.SetColor("_Color", temp);
    }


    void IncreaseGravity()
    {
        gravity += gravityIncrease;
        if (gravity > maxGravity) gravity = maxGravity;
    }

}
