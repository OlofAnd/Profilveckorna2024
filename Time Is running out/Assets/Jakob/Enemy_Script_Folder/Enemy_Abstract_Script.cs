using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy_Abstract_Script : MonoBehaviour
{
    public float EnemyHealthPoints { get; set; }
    public float EnemyDamage { get; set; }
    public abstract void EnemyBehaviour();
}
