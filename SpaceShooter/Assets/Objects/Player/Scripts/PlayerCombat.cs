using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float _fireRate = 0.3f;
    private float _nextFire = 0f;

    [SerializeField] private BulletPool _bulletPool;

    private void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
        {
            Fire();
        }
    }

    public void Fire()
    {
        if (_nextFire > Time.time) return;

        Bullet bullet = _bulletPool.GetBullet().GetComponent<Bullet>();
        bullet.Fire(transform.forward);

        _nextFire = Time.time + _fireRate;
    }
}
