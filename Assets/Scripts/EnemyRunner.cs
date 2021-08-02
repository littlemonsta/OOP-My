using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//INHERITENCE
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
    //POLYMORPHISM
    public override void UpdateEnemyData()
    {
        enemyHealth = 2;
        attackDelay = 2;
        enemyAttack = 10;
    }
}
