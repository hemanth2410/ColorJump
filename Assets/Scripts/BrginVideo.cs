using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.IO;
public class BrginVideo : MonoBehaviour
{
    VideoPlayer vplayer;
    // Start is called before the first frame update
    void Start()
    {
        vplayer = GetComponent<VideoPlayer>();
        vplayer.url = Path.Combine(Application.streamingAssetsPath, "VideoTutorial.mp4");
        vplayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
