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
    public int healToMax;       //inlagd
    public int attackRange;     //inlagd
    public int bulletSpeed;     //inlagd
    public int fireRate;        //inlagd
    public int tier1Time;       //inlagd

    [Header("Tier 2 Powerups")]
    public int bulleSpread;     //inlagd
    public int maxHp;           //inlagd
    public int damage;          //inlagd
    public int piercingRounds;  //inlagd
    public float bombRounds;    //inlagd
    public int tier2Time;

    [Header("Tier 3 Powerups")]
    public bool freezingAura;   //inlagd
    public bool rage;           //inlagd
    public bool phoenix;        //inlagd
    public bool tank;           //inlagd
    public int tier3Time;       //inlagd


    List<ScriptableObject> card_List = new List<ScriptableObject>();

    //public void Print()
    //{
    //    Debug.Log(name);
    //}
}
