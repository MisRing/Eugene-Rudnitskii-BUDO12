using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private int _minCount;
    [SerializeField] private GameObject _bulletPref;

    private Queue<GameObject> _bulletsQ = new Queue<GameObject>();

    private void Start()
    {
        for(int i = 0; i < _minCount; i++)
        {
            CreateBullet();
        }
    }

    private void CreateBullet()
    {
        GameObject bullet = Instantiate(_bulletPref, transform);
        bullet.SetActive(false);
        _bulletsQ.Enqueue(bullet);
    }

    public GameObject GetBullet()
    {
        if(_bulletsQ.Count == 0)
        {
            CreateBullet();
        }

        GameObject bullet = _bulletsQ.Dequeue();
        bullet.transform.SetParent(null);
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
        Bullet bulletComp = bullet.GetComponent<Bullet>();
        bulletComp.Return += ReturnBullet;

        return bullet;
    }

    private void ReturnBullet(GameObject bullet)
    {
        bullet.transform.SetParent(transform);
        bullet.transform.position = transform.position;
        Bullet bulletComp = bullet.GetComponent<Bullet>();
        bulletComp.Return -= ReturnBullet;
        bullet.SetActive(false);
        _bulletsQ.Enqueue(bullet);
    }
}
