using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy_Abstract_Script : MonoBehaviour
{
    public float EnemyHealthPoints { get; set; }
    public float Damage { get; set; }
    public int DifficultyValue { get; set; }
    public float ScoreValue { get; set; }
    public abstract void EnemyBehaviour();
}
