using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemy
{
    public void Start()
    {
        base.Start();
        _Speed = 0.7f;
        UpdateAgentSpeed();
        //UpdateEnemyData();

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

    }

    public override void UpdateEnemyData()
    {
        enemyHealth = 20;
        attackDelay = 2.5f;
        enemyAttack = 15;
    }
}
