using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Card",menuName="Card")]

public class Card_Information_Script : ScriptableObject
{
    [Header("Info om kort")]
    public new string name;
    public Sprite artwork;

    [Header("Tier 1 Powerups")]
    public int healToMax;
    public int attackRange;
    public int bulletSpeed;
    public int fireRate;
    public int tier1Time;

    [Header("Tier 2 Powerups")]
    public int bulleSpread;
    public int maxHp;
    public int damage;
    public int piercingRounds;
    public float bombRounds;

    [Header("Tier 3 Powerups")]
    public bool freezingAura;
    public bool rage;
    public bool phoenix;
    public bool tank;

    //public void Print()
    //{
    //    Debug.Log(name);
    //}
}
