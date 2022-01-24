using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer instance {get; set; }
    public Image mask;
    float originalSize;
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
        
    }

    // Update is called once per frame
    public void SetTimer(bool timerOn)
    {
        timerIsRunning = true;
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * (timeRemaining/10));
                }
            else 
            {
                timeRemaining = 0;
                timerIsRunning = false;
            }
            
        }
    }

    void Update()
    {

    }
}
