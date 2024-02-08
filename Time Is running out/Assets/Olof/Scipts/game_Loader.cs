using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class game_Loader : MonoBehaviour
{
    [Header("Tier 1")]
    [SerializeField] public ScriptableObject healToMax;
    [SerializeField] public ScriptableObject attackRange;
    [SerializeField] public ScriptableObject bulletSpeed;
    [SerializeField] public ScriptableObject fireRate;
    [SerializeField] public ScriptableObject tier1Time;

    [Header("Tier 2")]
    [SerializeField] public ScriptableObject bulletSpread;
    [SerializeField] public ScriptableObject maxHp;
    [SerializeField] public ScriptableObject damage;
    [SerializeField] public ScriptableObject piercingRounds;
    [SerializeField] public ScriptableObject bombRounds;
    [SerializeField] public ScriptableObject tier2Time;

    [Header("Tier 3")]
    [SerializeField] public ScriptableObject freezingAura;
    [SerializeField] public ScriptableObject rage;
    [SerializeField] public ScriptableObject phoenix;
    [SerializeField] public ScriptableObject tank;
    [SerializeField] public ScriptableObject tier3Time;

 
}
