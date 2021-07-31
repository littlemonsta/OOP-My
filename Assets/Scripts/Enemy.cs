using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent _Agent;
    private PlayerControl player;
    float navSpeed = 3.0f;
    float playerDistance;
    public int enemyHealth;
    private float attackDelay =1.5f;
    private bool canAttack = true;
    public bool enemyIsAlive = true;
    private int enemyAttack = 3;

    // Start is called before the first frame update
    void Start()
    {
           _Agent = GetComponent<NavMeshAgent>();
            player = GameObject.Find("Player").GetComponent<PlayerControl>();
            _Agent.speed = navSpeed;
            _Agent.acceleration = 999;
            _Agent.angularSpeed = 999;
            enemyHealth = 10;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerpos = player.transform.position;
        
        if (player !=null)
        {
            _Agent.SetDestination(playerpos);
            _Agent.isStopped = false;
            playerDistance = Vector3.Distance(playerpos, this.transform.position);
            
            
            if (playerDistance <= 2)
            {
                _Agent.isStopped = true;
            }
        }
        if (enemyHealth == 0 && enemyIsAlive)
        {
            enemyIsAlive = false;
            EnemyDeath();
        }
        if (canAttack && playerDistance < 3)
        {
            Debug.Log("Attacking Player");
            AddDamage(enemyAttack);
            StartCoroutine(AttackCooldown());
        }

    }

    void EnemyDeath()
    {
        Destroy(gameObject, 0.2f);
        Debug.Log("enemy died");
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }

    void AddDamage (int Damage)
    {
        Debug.Log("Deducting playerhealth");
        player.playerHealth -= Damage;
    }

    public void EnemyHit()
    {
        Debug.Log("Deducting enemyhealth");
        enemyHealth--;
    }

}
