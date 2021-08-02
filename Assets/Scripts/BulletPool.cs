using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    //ENCAPSULATION
    public static BulletPool Instance { get; private set; }

    [SerializeField] private Bullet bulletPrefab;

    private Queue<Bullet> bulletsAvailable = new Queue<Bullet>();

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        AddBullets(30);
    }

    public Bullet Get()
    {
        if (bulletsAvailable.Count == 0)
        {
            AddBullets(1);
        }

        return bulletsAvailable.Dequeue();
    }

    private void AddBullets(int count)
    {
        for (int i=0; i< count; i++)
        {
            var _bullet = Instantiate(bulletPrefab);
            _bullet.gameObject.SetActive(false);
            bulletsAvailable.Enqueue(_bullet);
        }
        //var _bullet = Instantiate(bulletPrefab);
        //return _bullet;
    }

    public void ReturnBullets(Bullet _bullet)
    {
        bulletsAvailable.Enqueue(_bullet);
    }
}
