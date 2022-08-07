using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioUtility : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Button MuteButton;
    [SerializeField] Sprite MutedSprite;
    [SerializeField] Sprite UnMutedSprite;
    [SerializeField] Image TargetImage;
    bool audioMuted;
    // Start is called before the first frame update
    void Start()
    {
        TargetImage.sprite = audioMuted ? UnMutedSprite : MutedSprite;

        MuteButton.onClick.AddListener(() =>
        {
            audioMuted = !audioMuted;
            TargetImage.sprite = audioMuted ? UnMutedSprite : MutedSprite;
            float volume = audioMuted ? -80.0f : 0.0f;
            audioMixer.SetFloat("MasterVolume", volume);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
