using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{


    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public GameObject DeadEffectPanel;
    public GameObject GameOverPanel;
    public GameObject TouchToStargGame;
    [SerializeField] GameObject HighScoreObject;
    [SerializeField] Animator highScoreAnimator;
    [SerializeField] GameObject gameoverButton;
    [SerializeField] GameObject MainMenuButton;
    [SerializeField] GameObject[] CheeringParticles;
    [SerializeField] Vector3 SPawnPos;
    Camera Main;
    public int score = 0;
    int previousBestScore;
    void Awake()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1;
        bestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        previousBestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    void Start()
    {
        scoreText.text = score.ToString();
        Main = Camera.main;
    }


    public void StartGame()
    {
        TouchToStargGame.SetActive(false);
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.color = new Color(Camera.main.backgroundColor.r + 0.1f, Camera.main.backgroundColor.g + 0.1f, Camera.main.backgroundColor.b + 0.1f, 0.2f);
        scoreText.text = score.ToString();


        if (score > PlayerPrefs.GetInt("BestScore", 0))
        {
            bestScoreText.text = score.ToString();
            PlayerPrefs.SetInt("BestScore", score);
        }
        if(score % 10 == 0 && score != 0)
        {
            int v= (score / 10) - 1;
            if(v < CheeringParticles.Length)
            {
                GameObject Prefab = CheeringParticles[v];
                var k = Instantiate(Prefab, Main.transform);
                k.transform.localPosition = SPawnPos;
            }
            

        }
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }

    IEnumerator GameOverCoroutine()
    {
        Time.timeScale = 0.1f;
        DeadEffectPanel.SetActive(true);
        scoreText.color = Color.white;

        yield return new WaitForSecondsRealtime(1.0f);
        Time.timeScale = 1.0f;
        DeadEffectPanel.SetActive(false);
        GameOverPanel.SetActive(true);
        if(score > previousBestScore && score > 50)
        {
            highScoreAnimator.SetTrigger("GoodScore");
            HighScoreObject.SetActive(true);
        }
        else
        {
            gameoverButton.SetActive(true);
            MainMenuButton.SetActive(true);
        }
        yield break;
    }




    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
