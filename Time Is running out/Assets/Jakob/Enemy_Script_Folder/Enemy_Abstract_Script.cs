using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy_Abstract_Script : MonoBehaviour
{
    public float EnemyHealthPoints { get; set; }
    public float Damage { get; set; }
    public float ScoreValue { get; set; }
    public int Wave
    {
        get
        {
            return FindObjectOfType<GameController_Script>().Wave;
        }
    }
    public int PlayerDamage
    {
        get
        {
            return FindObjectOfType<Player_Script>().playerDamage;
        }
    }
    public float EnemySpawnCooldown;
    public abstract void EnemyBehaviour();
}
