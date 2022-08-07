using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class SubmitHighScore : MonoBehaviour
{
    string URL = "http://dreamlo.com/lb/GixXcnnQ0UiH3e_mMx6nVgdmYhLb6ITkCTUfH1vYbx0A/add/";
    [SerializeField] TMP_InputField nameInputField;
    GameManager gameManager;
    [SerializeField] GameObject RestartButton;
    [SerializeField] GameObject MainMenuButton;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void SubmitScore()
    {
        if(!string.IsNullOrEmpty(nameInputField.text))
        {
            string dataUrl = URL + nameInputField.text + "/" + gameManager.score;
            UnityWebRequest webRequest = UnityWebRequest.Get(dataUrl);
            webRequest.SendWebRequest();
            while(!webRequest.isDone)
            {
                await System.Threading.Tasks.Task.Yield();
            }
            RestartButton.SetActive(true);
            MainMenuButton.SetActive(true);
        }
    }
}
