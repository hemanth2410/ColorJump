using Assets.Scripts;
using Lofelt.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationBehaviour : MonoBehaviour
{
    HapticSource vibrationSource;
    internal float maxHeight { get; set; }
    int spectrumIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        vibrationSource = GetComponent<HapticSource>();
        maxHeight = 10;
    }

    // Update is called once per frame
    void Update()
    {
        var desiredScale = 1 + AudioManager.GetSpectrumValue(spectrumIndex) * maxHeight;

        if(desiredScale / maxHeight > 0.85f)
        {
            vibrationSource.Play();
        }
    }
}
