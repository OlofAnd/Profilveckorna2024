using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Script : MonoBehaviour
{
    //[SerializeField] Player_Script playerScript;

    public Slider slider;


    void Update()
    {
        
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue= health;
        slider.value= health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
