using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Card",menuName="Card")]
public class Card_Information_Script : ScriptableObject
{
    [Header("Info om kort")]
    public new string name;
    public Sprite artwork;
    public Sprite cardBackground;

    [Header("Tier 1 Powerups")]
    public bool healToMax;       //inlagd
    public bool attackRange;     //inlagd
    public bool bulletSpeed;     //inlagd
    public bool fireRate;        //inlagd
    public bool tier1Time;       //inlagd

    [Header("Tier 2 Powerups")]
    public bool bulleSpread;     //inlagd
    public bool maxHp;           //inlagd
    public bool damage;          //inlagd
    public bool piercingRounds;  //inlagd
    public bool bombRounds;    //inlagd
    public bool tier2Time;

    [Header("Tier 3 Powerups")]
    public bool freezingAura;   //inlagd
    public bool rage;           //inlagd
    public bool phoenix;        //inlagd
    public bool tank;           //inlagd
    public bool tier3Time;       //inlagd


    List<ScriptableObject> card_List = new List<ScriptableObject>();

    //public void Print()
    //{
    //    Debug.Log(name);
    //}
}
