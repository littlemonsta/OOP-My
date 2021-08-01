using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunner : Enemy
{
    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        _Speed = 4f;
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
        enemyHealth = 2;
        attackDelay = 2;
        enemyAttack = 10;
    }
}
