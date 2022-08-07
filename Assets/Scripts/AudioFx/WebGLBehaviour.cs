using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void burst();
public class WebGLBehaviour : MonoBehaviour
{

    public static WebGLBehaviour Instance;
    [SerializeField] float decay;
    public float value;
    public event burst ParticleBurst;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(value > 0.0f)
        {
            value -= Time.deltaTime * decay;
        }
    }

    public void ResetValue()
    {
        value = 1;
        ParticleBurst?.Invoke();
    }
}
