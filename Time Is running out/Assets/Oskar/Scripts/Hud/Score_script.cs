using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score_script : MonoBehaviour
{
    [SerializeField] Player_Script player_Script;
    [SerializeField] Tank_Script tank_Script;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] float amountScore;


    void Start()
    {

    }

    void Update()
    {
        amountScore = player_Script.currentHealth;
        //if (tank_Script.isTankActive)
        //    amountScore = player_Script.tankMaxHealth;
        //else
        //    amountScore = player_Script.maxHealth;
        scoreText.text = "Hp: " + amountScore.ToString();
    }
}
