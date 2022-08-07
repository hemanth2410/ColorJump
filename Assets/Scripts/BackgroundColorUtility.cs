using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorUtility : MonoBehaviour
{
    Camera cam;
    [SerializeField] float timeBetweenColors;
    float timer = 0;
    Color newColor;
    Color currentColor;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        cam.backgroundColor = Random.ColorHSV();
        currentColor = cam.backgroundColor;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > timeBetweenColors)
        {
            newColor = Random.ColorHSV();
            timer = 0;
            currentColor = cam.backgroundColor;
        }
        cam.backgroundColor = Color.Lerp(currentColor, newColor, timer / timeBetweenColors);

    }
}
