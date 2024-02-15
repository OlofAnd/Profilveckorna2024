using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar_Script : MonoBehaviour
{
    public Slider timerSlider;
    public Gradient timeGradient;
    public Image timerFill;

    public void SetMaxTime(float time)
    {
        timerSlider.maxValue = time;
        timerSlider.value = time;

        timerFill.color = timeGradient.Evaluate(1f);
    }
    public void SetTime(float time)
    {
        timerSlider.value = time;

        timerFill.color = timeGradient.Evaluate(timerSlider.normalizedValue);
    }
}
