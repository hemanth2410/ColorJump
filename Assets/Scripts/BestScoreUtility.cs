using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BestScoreUtility : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI BestscoreText;
    // Start is called before the first frame update
    void Start()
    {
        BestscoreText.text = "BEST SCORE : " + PlayerPrefs.GetInt("BestScore",0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
