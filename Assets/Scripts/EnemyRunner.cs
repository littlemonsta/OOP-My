
using UnityEngine;
//INHERITENCE
public class EnemyRunner : Enemy

{
    private Animator _animatorMummy;
    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        _Speed = 2f;
        UpdateAgentSpeed();
        _animatorMummy = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        
        if (!_Agent.isStopped)
        {
            _animatorMummy.SetFloat("Movement", 1.1f, 0.1f, Time.deltaTime);
        }
        else
        {
            _animatorMummy.SetFloat("Movement", 0.1f, 0.1f, Time.deltaTime);
        }
        if (isAttacking)
        {
            _animatorMummy.SetBool("attack",true);
        }
        else { _animatorMummy.SetBool("attack", false); }
    }
    //POLYMORPHISM
    public override void UpdateEnemyData()
    {
        enemyHealth = 2;
        attackDelay = 2;
        enemyAttack = 10;
    }

    public override void EnemyHit()
    {
        base.EnemyHit();
        _animatorMummy.Play("takeDamage");
    }

    public override void EnemyDeath()
    {

        _animatorMummy.SetBool("die01", true);
        Destroy(gameObject, 2.5f);
    }
}
