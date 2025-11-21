using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    [SerializeField] private LayerMask _targets;
    [SerializeField] private PooledObjectType _objectType;
    [HideInInspector] public ObjectPool ObjectPool;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject || ((1 << collision.gameObject.layer) & _targets) == 0) return;

        DestroyThis();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject || ((1 << other.gameObject.layer) & _targets) == 0) return;

        DestroyThis();
    }

    public void DestroyThis()
    {
        ObjectPool.GetObject(_objectType, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
