using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public NavMeshAgent _Agent;
    public PlayerControl player;
    public Obstacle obstacle;
    public float _Speed = 2f;
    public bool enemyIsAlive = true;
    public int enemyAttack = 3;
    public float attackDelay =1.5f;
    public int enemyHealth;
    private float navSpeed = 3.5f;
    private float time;
    private float enemyMaxZ = 100f;
    private float enemyMinZ = 0f;
    public float TimeToCheck { get; set; }
    private float playerDistance;

    private bool canAttack = true;
    private bool isColObstacle = false;
    
    
    //ENCAPSULATION
    private float _attackDistance;
    public float GetAttackDistance()
    {
        return (_attackDistance);
    }

    public void SetAttackDistance(float i)
    {
        _attackDistance = i;
    }


    
    // Start is called before the first frame update
    protected void Start()
    {
            _Speed = 2f;
            _Agent = GetComponent<NavMeshAgent>();
            player = GameObject.Find("Player").GetComponent<PlayerControl>();
            UpdateAgentSpeed();
            UpdateEnemyData();
            SetAttackDistance(3);
            TimeToCheck=7;

    }

    // Update is called once per frame
    protected void Update()
    {
        time = Time.time;
        
        if (player !=null)
        {
            MoveEnemy();
        }
        if (enemyHealth <= 0 )
        {
            //enemyIsAlive = false;
            EnemyDeath();
        }
        if (canAttack && playerDistance <= GetAttackDistance())
        {
            //Debug.Log("Attacking Player");
            AddDamage(enemyAttack);
            StartCoroutine(AttackCooldown());
        }

        if (isColObstacle)
        {
            AddDamageObstacle(enemyAttack);
            StartCoroutine(AttackCooldown());
        }
        // check if nav agent is stuck
        if (time>TimeToCheck)
        {
            TimeToCheck = Time.time + TimeToCheck;
            if (!_Agent.hasPath && _Agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                //Debug.Log("Character stuck");
                _Agent.enabled = false;
                _Agent.enabled = true;
                MoveEnemy();
                //Debug.Log("navmesh re enabled");
            }

        }
       
    }
    // Polymorphism
    public virtual void UpdateEnemyData()
    {
        enemyAttack = 5;
        attackDelay = 1.5f;
        enemyHealth = 2;
        
    }

    public void UpdateAgentSpeed()
    {
        _Agent.speed = navSpeed * _Speed;
        _Agent.acceleration = 999 * _Speed;
        _Agent.angularSpeed = 999;
        
    }

    public void MoveEnemy()
    {
        Vector3 playerpos = player.transform.position;
        _Agent.SetDestination(playerpos);
        _Agent.isStopped = false;
        playerDistance = Vector3.Distance(playerpos, this.transform.position);


        if (playerDistance < GetAttackDistance())
        {
            _Agent.isStopped = true;
        }

        if (transform.position.z < enemyMinZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, enemyMinZ);
        }
        if (transform.position.z > enemyMaxZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, enemyMaxZ);
        }
        if (transform.position.x < PlayerControl.minX)
        {
            transform.position = new Vector3(PlayerControl.minX, transform.position.y, transform.position.z);
        }
        if (transform.position.x > PlayerControl.maxX)
        {
            transform.position = new Vector3(PlayerControl.maxX, transform.position.y, transform.position.z);
        }
    }
    void EnemyDeath()
    {
        Destroy(gameObject, 0.2f);
        //Debug.Log("enemy died");
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }

    void AddDamage (int Damage)
    {
       // Debug.Log("Deducting playerhealth"+ player.playerHealth);
        player.playerHealth -= Damage;
    }

      void AddDamageObstacle (int Damage)
    {
        //Debug.Log("Deducting obstacle"+ obstacle.health);
        obstacle.health -= Damage;
        isColObstacle = false;
    }

    public void EnemyHit()
    {
        //Debug.Log("Deducting enemyhealth"+enemyHealth);
        enemyHealth--;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isColObstacle = true;
            obstacle = collision.gameObject.GetComponent<Obstacle>();
            //Debug.Log("coliided obstacle" + obstacle.health);
        }
    }

}
