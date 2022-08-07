using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CustomUiCOlorEffect : MonoBehaviour
{
    [SerializeField] Color[] colors;
    [SerializeField] float timeBetweenColors;
    [SerializeField] TextMeshProUGUI tmproUgui;
    float timer = 0;
    Color newColor;
    Color prevCOlor;
    // Start is called before the first frame update
    void Start()
    {
        tmproUgui = GetComponent<TextMeshProUGUI>();
        tmproUgui.color = colors[Random.Range(0, colors.Length)];
        prevCOlor = tmproUgui.color;
        newColor = colors[Random.Range(0, colors.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= timeBetweenColors)
        {
            timer += Time.deltaTime;
            tmproUgui.color = Color.Lerp(prevCOlor, newColor, timer / timeBetweenColors);
        }
        if (timer > timeBetweenColors)
        {
            prevCOlor = tmproUgui.color;
            timer = 0;
            newColor = colors[Random.Range(0, colors.Length)];
        }
    }
}
