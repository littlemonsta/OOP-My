
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int health = 100;
    // Start is called before the first frame update
 
    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject, 0.5f);
        }
    }

    public void ObstacleHit()
    {
        health--;
    }

  
}
