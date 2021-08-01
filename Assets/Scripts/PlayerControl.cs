using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float _MoveSpeed = 10.0f;
    [SerializeField] private GameObject bulletprefab;
    private Transform gunend;
    public int playerHealth;
    public static float minZ = -2f;
    public static float maxZ = 50f;
    public static float minX = -19f;
    public static float maxX = 19f;
    private AudioSource playerAudio;
    [SerializeField] private AudioClip gunfire;

    // Start is called before the first frame update
    void Awake()
    {
        playerHealth = 50;
        playerAudio = GetComponent<AudioSource>();
        gunend = GameObject.Find("gunend").transform;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Shoot();
        if (playerHealth <= 0)
        {
            GameOver();
        }
    }

    void MovePlayer()
    {
        float verticalMove = Input.GetAxis("Vertical");
        float horizontalMove = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * verticalMove * Time.deltaTime * _MoveSpeed);
        transform.Translate(Vector3.right * horizontalMove * Time.deltaTime * _MoveSpeed);

        //keep player in bounds
        if (transform.position.z < minZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, minZ);
        }
        if (transform.position.z > maxZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxZ);
        }
        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }
        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }

    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletprefab, gunend.transform.position, bulletprefab.transform.rotation);
            playerAudio.PlayOneShot(gunfire,0.6f);
        }
    }

    void GameOver()
    {
        //Debug.Log("You have died");
        SceneManager.LoadScene("GameOver");

    }

}
