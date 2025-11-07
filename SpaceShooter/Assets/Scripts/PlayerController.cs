using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _flySpeed = 1f;
    [SerializeField] private float _tilt = 4f;
    [SerializeField] private float _fireRate = 0.3f;
    private float _nextFire = 0f;

    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private Boundary _boundary;

    private Rigidbody _rb;

    private Vector3 _movement;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        _movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if(_nextFire <= Time.time && (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)))
        {
            Bullet bullet = _bulletPool.GetBullet().GetComponent<Bullet>();
            bullet.Fire(transform.forward);

            _nextFire = Time.time + _fireRate;
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movement * _flySpeed;

        if (_boundary)
        {
            _rb.position = _boundary.LimitObject(_rb.position);
        }

        _rb.rotation = Quaternion.Euler(0f, 0f, _rb.velocity.x * -_tilt);
    }
}
