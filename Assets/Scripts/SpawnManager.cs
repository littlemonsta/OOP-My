using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    private int waveNumber = 0;
    [SerializeField] private int maxSpawn = 15;
    [SerializeField] private GameObject gruntPrefab;
    [SerializeField] private GameObject runnerPrefab;
    [SerializeField] private GameObject tankPrefab;
    private float delaySpawn = 7f;
    private int enemyOnField;
    private int runnerCount=0;
    private int tankCount=0;
    bool canSpawn = false;
    int spawnCount =100 ;
    private AudioSource spawnAudio;
    [SerializeField] private AudioClip prepare;
    [SerializeField] private TextMeshProUGUI wavetext;


    // Start is called before the first frame update
    void Start()
    {
        spawnAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
        enemyOnField = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyOnField == 0 && (spawnCount>=maxSpawn))
        {

            WaveLoad();
            runnerCount = 0;
            tankCount = 0;
            spawnCount = 0;
            runnerCount = 0;
            tankCount = 0;
            maxSpawn += 5;
            SpawnMobs();
            //Debug.Log("wavenumber :"+waveNumber);

        }

        if (spawnCount < maxSpawn)
        {
            SpawnMobs();
        }
    }

    void WaveLoad()
    {
        waveNumber++;
        wavetext.text = "Wave " + waveNumber;
        wavetext.gameObject.SetActive(true);
        spawnAudio.PlayOneShot(prepare);
        StartCoroutine(SpawnDelay(5f));
        

    }
    void SpawnMobs()
    {
         
                if (canSpawn)
                 {
                canSpawn = false;
                for (int j = 0; j < 5; j++)
                {
                    int randomize = Random.Range(-18, 18);
                    Instantiate(gruntPrefab, new Vector3(randomize,transform.position.y,transform.position.z), gruntPrefab.transform.rotation);
                    spawnCount++;
                }
                if (waveNumber > 1 && runnerCount< waveNumber)
                {
                    int randomize = Random.Range(-18, 18);
                    Instantiate(runnerPrefab, new Vector3(randomize, transform.position.y, transform.position.z), runnerPrefab.transform.rotation);
                    spawnCount++;
                    runnerCount++;
                }
                if (waveNumber > 2 && tankCount < waveNumber)
                {
                    int randomize = Random.Range(-18, 18);
                    Instantiate(tankPrefab, new Vector3(randomize, transform.position.y, transform.position.z), tankPrefab.transform.rotation);
                     spawnCount++;
                    tankCount++;
                }
                StartCoroutine(SpawnDelay(delaySpawn));
            Debug.Log("z pos =" + transform.position.z);
        }
        
    }

    IEnumerator SpawnDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("i did pause "+delay);
        wavetext.gameObject.SetActive(false);
        canSpawn = true;
    }
}
