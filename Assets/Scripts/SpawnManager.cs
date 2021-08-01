using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int waveNumber=0;
    [SerializeField] private int maxSpawn = 20;
    [SerializeField] private GameObject gruntPrefab;
    [SerializeField] private GameObject runnerPrefab;
    [SerializeField] private GameObject tankPrefab;
    private float delaySpawn = 10 ;
    private int enemyOnField;
    private int runnerCount=0;
    private int tankCount=0;
    bool canSpawn = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        enemyOnField = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyOnField == 0)
        {   
            
            waveNumber++;
            runnerCount = 0;
            tankCount = 0;
            SpawnMobs();
        }
    }

    void SpawnMobs()
    {
        for (int i = 0; i < maxSpawn / 10; i++)
        {
            //pause every 10 spawn enemies
            //i 

            for (int j = 0; j < 5; j++)
            {
                Instantiate(gruntPrefab, transform.position, gruntPrefab.transform.rotation);
            }
            StartCoroutine(SpawnDelay());

        }
/*
                if (j == 9)
                {
                    StartCoroutine(SpawnDelay());
                }
                
                if (waveNumber > 1 && runnerCount <= waveNumber)
                {
                    Instantiate(runnerPrefab, transform.position, runnerPrefab.transform.rotation);
                    i++;
                    runnerCount++;
                }
                else if (waveNumber > 2 && tankCount <= waveNumber)
                {
                    Instantiate(tankPrefab, transform.position, tankPrefab.transform.rotation);
                    tankCount++;
                    i++;
                }
                Debug.Log("j is " + j);
                
                
            }



            Instantiate(gruntPrefab, transform.position, gruntPrefab.transform.rotation);
            Debug.Log("i is " + i);*/
        
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(delaySpawn);
        Debug.Log("i did pause");
    }
}
