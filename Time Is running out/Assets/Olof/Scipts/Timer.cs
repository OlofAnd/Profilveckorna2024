using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
   [SerializeField] float remainingTime;


    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            //Game OVER
            timerText.color = Color.red;
            //fixa så att timer blir röd när tiden är låg.
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }



    //public void Pause()
    //{
    //    pauseMenu.SetActive(true);
    //    Time.timeScale = 0;
    //}

    //public void Resume()
    //{
    //    pauseMenu.SetActive(false);
    //    Time.timeScale = !;
    //}
}
