using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private int _baseCount;
    [SerializeField] private GameObject _bulletPref;
    private Queue<GameObject> _bulletsQ;

    private void Awake()
    {
        _bulletsQ = new Queue<GameObject>();

        for (int i = 0; i < _baseCount; i++)
        {
            AddBullet();
        }
    }

    private void AddBullet()
    {
        GameObject newBullet = Instantiate(_bulletPref, transform);
        newBullet.GetComponent<FireBall>().OnHitVoid = ReturnBullet;
        newBullet.SetActive(false);

        _bulletsQ.Enqueue(newBullet);
    }

    public void GetBullet()
    {
        if(_bulletsQ.Count == 0)
        {
            AddBullet();
        }

        GameObject bullet = _bulletsQ.Dequeue();
        bullet.transform.SetParent(null);
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
    }

    public void ReturnBullet(GameObject bullet)
    {
        if (_bulletsQ.Contains(bullet))
            return;

        _bulletsQ.Enqueue(bullet);
        bullet.transform.SetParent(transform);
        bullet.SetActive(false);
    }
}
