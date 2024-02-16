using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    [SerializeField] GameController_Script gameController_Script;
    public TimerBar_Script timerBarInTimer;

    void Update()
    {
        if (gameController_Script.enemiesAlive > 0)
        {
            if (gameController_Script.remainingTime > 0)
            {
                gameController_Script.remainingTime -= Time.deltaTime;
                timerBarInTimer.SetTime(gameController_Script.remainingTime);
            }
            else if (gameController_Script.remainingTime < 0)
            {
                gameController_Script.remainingTime = 0;
                //Game OVER
                timerText.color = Color.red;
                //fixa så att timer blir röd när tiden är låg.
            }
        }
        int minutes = Mathf.FloorToInt(gameController_Script.remainingTime / 60);
        int seconds = Mathf.FloorToInt(gameController_Script.remainingTime % 60);
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
