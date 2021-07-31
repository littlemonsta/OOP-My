using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float _MoveSpeed = 10.0f;
    public GameObject bulletprefab;
    public int playerHealth;
    // Start is called before the first frame update
    void Awake()
    {
        playerHealth = 100;
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
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletprefab, transform.position, bulletprefab.transform.rotation);
        }
    }

    void GameOver()
    {
        Debug.Log("You have died");
    }
}
