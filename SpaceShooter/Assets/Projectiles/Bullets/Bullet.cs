using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IReturnable
{
    [SerializeField] private float _speed = 3f;

    public event Action<GameObject> Return;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }

    public void Fire(Vector3 direction)
    {
        direction = direction.normalized;
        transform.LookAt(transform.position + direction);

        _rb.velocity = transform.forward * _speed;
    }

    public void ReturnThis()
    {
        Return?.Invoke(gameObject);
    }
}
