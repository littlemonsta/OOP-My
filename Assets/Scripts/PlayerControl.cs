using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float _MoveSpeed = 10.0f;
    [SerializeField] private GameObject bulletprefab;
    [SerializeField] LayerMask cantAimHere;
    public Camera fpscamera;

    private Transform gunend;
    public int playerHealth;
    public static float minZ = -2f;
    public static float maxZ = 50f;
    public static float minX = -19f;
    public static float maxX = 19f;
    private AudioSource playerAudio;
    [SerializeField] private AudioClip gunfire;
    private Animator _animator;
    public Vector3 hitDirection;

    // Start is called before the first frame update
    void Awake()
    {
        playerHealth = 50;
        _animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        fpscamera = GetComponentInChildren<Camera>();
        gunend = GameObject.Find("gunend").transform;
    }

  
    // Update is called once per frame
    void Update()
    {
        AimToMouse();
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
        Vector3 movement = new Vector3(horizontalMove, 0, verticalMove);

        if (movement.magnitude>0)
        {
            movement.Normalize();
            movement *= _MoveSpeed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }

        float velocityZ = Vector3.Dot(movement.normalized, transform.forward);
        float velocityX = Vector3.Dot(movement.normalized, transform.right);
        _animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        _animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);


        //transform.Translate(Vector3.forward * verticalMove * Time.deltaTime * _MoveSpeed);
        //transform.Translate(Vector3.right * horizontalMove * Time.deltaTime * _MoveSpeed);

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
            var bulletpool = GameObjectPool.Instance.Get();
            if (bulletpool != null)
            {
                bulletpool.transform.position = gunend.transform.position;
                bulletpool.gameObject.SetActive(true);

                
                RaycastHit hit;

                Ray ray2 = fpscamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
                if (Physics.Raycast(ray2, out hit))
                {
                    hitDirection = hit.point;
                }
                else
                {
                    hitDirection = ray2.GetPoint(80);

                }
                hitDirection.y = gunend.position.y;
                
                Debug.DrawRay(gunend.transform.position, hitDirection);
                Vector3 tmpBull = hitDirection - gunend.transform.position;
                bulletpool.gameObject.transform.up = tmpBull.normalized;
            }

            //Instantiate(bulletprefab, gunend.transform.position, bulletprefab.transform.rotation);
            playerAudio.PlayOneShot(gunfire,0.6f);
        }
    }


    void GameOver()
    {
        //Debug.Log("You have died");
        SceneManager.LoadScene("GameOver");

    }

    void AimToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitinfo, Mathf.Infinity, cantAimHere))
        {
            var direction = hitinfo.point - transform.position;
            direction.y = 0;
            direction.Normalize();
            //hitDirection = hitinfo.point;
            transform.forward = direction;
        }
    }

}
