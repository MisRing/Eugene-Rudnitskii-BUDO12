using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private int _startPool;

    [SerializeField] private GameObject _bulletPref;
    private Queue<GameObject> _bulletsQ;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _bulletsQ = new Queue<GameObject>();

        for (int i = 0; i < _startPool; i++)
        {
            CreateBullet();
        }
    }

    private void CreateBullet()
    {
        GameObject bullet = Instantiate(_bulletPref);
        bullet.transform.SetParent(transform);
        bullet.transform.localPosition = Vector2.zero;
        bullet.SetActive(false);

        _bulletsQ.Enqueue(bullet);
    }

    public Bullet GetBullet(DamageData damageData, float bulletSpeed, bool isLookRight, Vector2 spawnPos, float timer = 3f)
    {
        if(_bulletsQ.Count == 0)
        {
            CreateBullet();
        }

        GameObject bullet = _bulletsQ.Dequeue();
        bullet.SetActive(true);
        bullet.transform.SetParent(null);
        bullet.transform.position = spawnPos;
        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        bulletComponent.Fire(damageData, bulletSpeed, isLookRight, timer);
        bulletComponent.OnHit += ReturnBullet;

        return bulletComponent;
    }

    public void ReturnBullet(GameObject bullet)
    {
        if (_bulletsQ.Contains(bullet)) return;

        _bulletsQ.Enqueue(bullet);

        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        bulletComponent.OnHit -= ReturnBullet;

        bullet.transform.SetParent(transform);
        bullet.transform.localPosition = Vector2.zero;
        bullet.SetActive(false);
    }

    
}
