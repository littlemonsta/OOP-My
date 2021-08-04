
using UnityEngine;
//INHERITENCE
public class Tank : Enemy
{
    private Animator _animatorTank;
    public void Start()
    {
        base.Start();
        _Speed = 0.75f;
        UpdateAgentSpeed();
        _animatorTank = GetComponent<Animator>();
        

    }

    // Update is called once per frame
   void Update()
    {
        base.Update();
        if (!_Agent.isStopped)
        {
            _animatorTank.SetFloat("a", 1.0f, 0.1f, Time.deltaTime);
        }
        else
        {
            _animatorTank.SetFloat("a", 0.1f, 0.1f, Time.deltaTime);
        }

    }

    public override void UpdateEnemyData()
    {
        enemyHealth = 10;
        attackDelay = 2.5f;
        enemyAttack = 15;
    }
}
