using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _Speed = 100;
    [SerializeField] float _BulletRange = 50;
    

    // Start is called before the first frame update
    void Start()
    {
        //enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        //obstacle = GameObject.FindGameObjectWithTag("Obstacle").GetComponent<Obstacle>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _Speed);
        if (transform.position.z > _BulletRange)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit");
            collision.gameObject.SendMessage("EnemyHit");
            Destroy(gameObject);

        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle hit");
            collision.gameObject.SendMessage("ObstacleHit");
            Destroy(gameObject);


        }

    }
}
