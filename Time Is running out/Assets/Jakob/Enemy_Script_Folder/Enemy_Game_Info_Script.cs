using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Game_Info_Script : MonoBehaviour
{
    [SerializeField] GameObject PlayerReff;
    public Vector2 PlayerPosition
    {
        get
        {
            return PlayerReff.transform.localPosition;
        }
    }
}
