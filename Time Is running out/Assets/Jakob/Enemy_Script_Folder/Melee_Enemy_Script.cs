using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 class Melee_Enemy_Script : Enemy_Abstract_Script
{

    void Start()
    {
        EnemyHealthPoints = 10f;
        EnemyDamage = 1f;
    }

    void Update()
    {
        EnemyBehaviour();
    }
    public override void EnemyBehaviour()
    {

    }
}
