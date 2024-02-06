using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Card",menuName="Cards/Tier1")]

public class Card_Information_Script : ScriptableObject
{
    // info om kortet
    public new string name;
    public Sprite artwork;

    // power ups tier 1
    public int healToMax;
    public int attackRange;
    public int bulletSpeed;
    public int fireRate;


    public void Print()
    {
        Debug.Log(name);
    }
}
