
using UnityEngine;

public class Bullet : MonoBehaviour, IGameObjectPooled
{
    [SerializeField] float _Speed = 20;
    [SerializeField] float _BulletRange = 50;
    
    public GameObjectPool Pool { get; set; }
    Vector3 launchPos;
    
    [SerializeField] private bool isMoving;
    float timeOnScreen;
    float currentTime;

  

    private void OnEnable()
    {
       
       // bulletRB = GetComponent<Rigidbody>();
       // bulletRB.angularVelocity = Vector3.zero;
       // bulletRB.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
           
           
            launchPos = transform.position;
            timeOnScreen = Time.time;
            
            isMoving = true;
          
        }
        currentTime = Time.time;
        transform.Translate(Vector3.up* _Speed * Time.deltaTime);
       
        float distanceTravel = transform.position.z - launchPos.z;
        if (currentTime - timeOnScreen >3)
        {
            this.gameObject.SetActive(false);
           
            Pool.ReturnObjectsToPool(this.gameObject);
            isMoving = false;
        }

        
        if (distanceTravel > _BulletRange)
        {
            
            this.gameObject.SetActive(false);
            
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
            
            Pool.ReturnObjectsToPool(this.gameObject);
            isMoving = false;

        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //Debug.Log("Obstacle hit");
            collision.gameObject.SendMessage("ObstacleHit");
           
            this.gameObject.SetActive(false);
            
            Pool.ReturnObjectsToPool(this.gameObject);
            isMoving = false;

        }

    }
}

 internal interface IGameObjectPooled
    {
        GameObjectPool Pool { get; set; }
    }
    


