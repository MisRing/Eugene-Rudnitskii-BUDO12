using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipCombat : MonoBehaviour
{
    [SerializeField] private protected float _fireRate = 0.3f;
    private protected float _nextFire = 0f;

    [SerializeField] private protected BulletPool _bulletPool;

    private protected void Fire()
    {
        if (_nextFire > Time.time) return;

        Bullet bullet = _bulletPool.GetBullet().GetComponent<Bullet>();
        bullet.Fire(transform.forward);

        _nextFire = Time.time + _fireRate;
    }
}
