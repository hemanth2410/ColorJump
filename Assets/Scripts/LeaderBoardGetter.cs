using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LeaderBoardGetter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI serialNumberText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI scoreText;
    
    public void SetLeaderBoardItem(string number, string name, string score)
    {
        serialNumberText.text = number;
        nameText.text = name;
        scoreText.text = score;

    }
}
