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
    private float navSpeed = 3.5f;
    public float _Speed = 2f;
    float playerDistance;

    private bool canAttack = true;
    public bool enemyIsAlive = true;
    public int enemyAttack = 3;
    public float attackDelay =1.5f;
    public int enemyHealth;
    private bool isColObstacle = false;
    private float timeToCheck = 8f;
    private float time;
    
    // Start is called before the first frame update
    protected void Start()
    {
            _Speed = 2f;
            _Agent = GetComponent<NavMeshAgent>();
            player = GameObject.Find("Player").GetComponent<PlayerControl>();
            UpdateAgentSpeed();
            UpdateEnemyData();
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
        if (canAttack && playerDistance <= 3)
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

        if (time>timeToCheck)
        {
            timeToCheck = Time.time + timeToCheck;
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


        if (playerDistance <= 3)
        {
            _Agent.isStopped = true;
        }

        if (transform.position.z < PlayerControl.minZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, PlayerControl.minZ);
        }
        if (transform.position.z > 100)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 100);
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
        Debug.Log("Deducting enemyhealth"+enemyHealth);
        enemyHealth--;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isColObstacle = true;
            obstacle = collision.gameObject.GetComponent<Obstacle>();
            Debug.Log("coliided obstacle" + obstacle.health);
        }
    }

}
