using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipCombat : MonoBehaviour
{
    [SerializeField] private protected float _fireRate = 0.3f;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private PooledObjectType _bulletType;
    private protected float _nextFire = 0f;

    [SerializeField] private protected ObjectPool _objectPool;

    private protected void Fire(Quaternion rotation)
    {
        if (_nextFire > Time.time) return;

        Bullet bullet = _objectPool.GetObject(_bulletType, _bulletSpawnPoint.position, rotation).GetComponent<Bullet>();
        bullet.Fire(transform.forward);

        _nextFire = Time.time + _fireRate;
    }
}
