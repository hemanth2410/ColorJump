using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LeaderBoardScoreGetter : MonoBehaviour
{
    [SerializeField] string leaderBoardURL;
    [SerializeField] GameObject leaderboardPrefab;
    [SerializeField] Transform leaderboardTransform;
    // Start is called before the first frame update
    void Start()
    {
        //GetScores();
    }

    private void OnEnable()
    {
        LeaderBoardGetter[] children = GetComponentsInChildren<LeaderBoardGetter>();
        foreach (var child in children)
        {
            Destroy(child.gameObject);
        }
        GetScores();
    }

    async void GetScores()
    {
        UnityWebRequest data = UnityWebRequest.Get(leaderBoardURL);
        data.SendWebRequest();
        
        while(!data.isDone)
        {
            await System.Threading.Tasks.Task.Yield();
        }
        byte[] downloadData = data.downloadHandler.data;
        string rawData = System.Text.Encoding.UTF8.GetString(downloadData);
        string[] lines = rawData.Split("\n");
        int num = lines.Length < 10 ? lines.Length : 10;
        for (int i = 0; i < num-1; i++)
        {
            var j = Instantiate(leaderboardPrefab, leaderboardTransform);
            string[] results = lines[i].Split("|");
            if(!string.IsNullOrEmpty(results[1]))
            {
                j.GetComponent<LeaderBoardGetter>().SetLeaderBoardItem((i+1).ToString(), results[0], results[1]);
            }
        }
    }
}
