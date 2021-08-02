using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IGameObjectPooled
{
    [SerializeField] float _Speed = 100;
    [SerializeField] float _BulletRange = 50;
    public GameObjectPool Pool { get; set; }
    Vector3 launchPos;
    // Start is called before the first frame update
    void Start()
    {
        launchPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 launchPos = transform.position;
        transform.Translate(Vector3.up * Time.deltaTime * _Speed);
        float distanceTravel = transform.position.z - launchPos.z;

        //Debug.Log("bullet travel " + distanceTravel);
        if (distanceTravel > _BulletRange)
        {
            //BulletPool.Instance.ReturnBullets(this);
            //Destroy(gameObject);
            Pool.ReturnObjectsToPool(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Enemy hit");
            collision.gameObject.SendMessage("EnemyHit");
            gameObject.SetActive(false);
            //BulletPool.Instance.ReturnBullets(this);
            Pool.ReturnObjectsToPool(this.gameObject);


        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //Debug.Log("Obstacle hit");
            collision.gameObject.SendMessage("ObstacleHit");
            //Destroy(gameObject);
            gameObject.SetActive(false);
            //BulletPool.Instance.ReturnBullets(this);
            Pool.ReturnObjectsToPool(this.gameObject);


        }

    }
}

 internal interface IGameObjectPooled
    {
        GameObjectPool Pool { get; set; }
    }
    


