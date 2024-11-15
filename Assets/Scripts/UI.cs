using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text timerText;
    private float time;
    private int min;
    private int sec;
    private bool isEnd;

    private void Start()
    {
        time = 30f;
        isEnd = false;
    }

    private void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
            time = 0;


        timerText.text = "Time: " + time.ToString("F1");

        if (time == 0)
        {
            GameManager.instance.EndGame();
        }

        //min = (int)time / 60;
        //sec = ((int)time - min * 60) % 60;

        //if (min <0 && sec <=0)
        //{
        //    if (isEnd == false)
        //    {
        //        isEnd = true;
        //        timerText.text = 0.ToString() + " : " + 0.ToString();
        //        GameManager.instance.EndGame();
        //    }
        //}
    }
}
