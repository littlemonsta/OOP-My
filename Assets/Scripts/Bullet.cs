using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IGameObjectPooled
{
    [SerializeField] float _Speed = 100f;
    [SerializeField] float _BulletRange = 50;
    private Rigidbody bulletRB;
    public GameObjectPool Pool { get; set; }
    Vector3 launchPos;
    Vector3 hitPos;
    [SerializeField] private bool isMoving;
    float timeOnScreen;
    float currentTime;

  

    private void OnEnable()
    {
       
        bulletRB = GetComponent<Rigidbody>();
        bulletRB.angularVelocity = Vector3.zero;
        bulletRB.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isMoving)
        {
           
            bulletRB.isKinematic = false;
            launchPos = transform.position;
            timeOnScreen = Time.time;
            hitPos = GameObject.Find("Player").GetComponent<PlayerControl>().hitDirection;
            isMoving = true;
            Debug.DrawLine(launchPos, hitPos, Color.red, 3 );
        }
        currentTime = Time.time;
        transform.Translate(Vector3.up* _Speed * Time.deltaTime);
        //bulletRB.AddForce(hitPos * _Speed);
        float distanceTravel = transform.position.z - launchPos.z;
        if (currentTime - timeOnScreen >3)
        {
            this.gameObject.SetActive(false);
            bulletRB.isKinematic = true;
            Pool.ReturnObjectsToPool(this.gameObject);
            isMoving = false;
        }

        
        if (distanceTravel > _BulletRange)
        {
            //BulletPool.Instance.ReturnBullets(this);
            this.gameObject.SetActive(false);
            bulletRB.isKinematic = true;
            Pool.ReturnObjectsToPool(this.gameObject);
            isMoving = false;
        }


    }
    
 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Enemy hit");
            collision.gameObject.SendMessage("EnemyHit");
            this.gameObject.SetActive(false);
            bulletRB.isKinematic = true;
            //BulletPool.Instance.ReturnBullets(this);
            Pool.ReturnObjectsToPool(this.gameObject);
            isMoving = false;

        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle hit");
            collision.gameObject.SendMessage("ObstacleHit");
            //Destroy(gameObject);
            bulletRB.isKinematic = true;
            this.gameObject.SetActive(false);
            //BulletPool.Instance.ReturnBullets(this);
            Pool.ReturnObjectsToPool(this.gameObject);
            isMoving = false;

        }

    }
}

 internal interface IGameObjectPooled
    {
        GameObjectPool Pool { get; set; }
    }
    


