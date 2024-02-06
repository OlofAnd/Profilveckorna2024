using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class game_Loader : MonoBehaviour
{
    [Header("Tier 1")]
    [SerializeField] public ScriptableObject healToMax;
    [SerializeField] public ScriptableObject attackRange;       //GÖR ALLA TILL SCRIPTABLE OBJECTS
    [SerializeField] public ScriptableObject bulletSpeed;
    [SerializeField] public ScriptableObject fireRate;
    [SerializeField] public ScriptableObject tier1Time;

    [Header("Tier 2")]
    [SerializeField] public ScriptableObject bulletSpread;
    [SerializeField] public ScriptableObject maxHp;
    [SerializeField] public ScriptableObject damage;
    [SerializeField] public ScriptableObject piercingRounds;
    [SerializeField] public ScriptableObject bombRounds;
    // lägg till time2

    [Header("Tier 3")]
    [SerializeField] public ScriptableObject freezingAura;
    [SerializeField] public ScriptableObject rage;
    [SerializeField] public ScriptableObject phoenix;
    [SerializeField] public ScriptableObject tank;
    // lägg till time3

    public List<GameObject> Cards = new List<GameObject>();
}
