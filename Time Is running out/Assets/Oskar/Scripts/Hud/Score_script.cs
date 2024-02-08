using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score_script : MonoBehaviour
{
    [SerializeField] Player_Script player_Script;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] float amountScore;


    void Start()
    {

    }

    void Update()
    {
        amountScore = player_Script.currentHealth;
        scoreText.text = "Hp: " + amountScore.ToString();
    }
}
