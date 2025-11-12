using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class ShipFlyComponent : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private protected float _flySpeed = 1f;
    [SerializeField] private protected float _tilt = 4f;
    [SerializeField] private protected Boundary _boundary;

    [Header("Dash Settings")]
    [SerializeField] private protected bool _canDash = true;
    [SerializeField] private protected float _dashForce = 15f;
    [SerializeField] private protected float _dashReduction = 30f;
    [SerializeField] private protected float _dashDuration = 0.2f;
    [SerializeField] private protected float _dashCooldown = 1f;

    private Rigidbody _rb;
    private Collider _collider;
    private protected Vector3 _movement;

    private protected bool _isDashing = false;
    private protected bool _dashOnCooldown = false;

    private protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        
        _collider = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        if (!_isDashing)
        {
            _rb.velocity = _movement * _flySpeed;
            if(_rb.rotation.eulerAngles.y == 0)
            {
                _rb.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, _rb.velocity.x * -_tilt);
            }
            else
            {
                _rb.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, _rb.velocity.x * _tilt);
            }
        }

        if (_boundary)
        {
            _rb.position = LimitByBoundary(_rb.position);
        }
    }

    private Vector3 LimitByBoundary(Vector3 position)
    {
        position = new Vector3(
            Mathf.Clamp(position.x, _boundary.MinX, _boundary.MaxX),
            position.y,
            Mathf.Clamp(position.z, _boundary.MinZ, _boundary.MaxZ));
        
        return position;
    }

    private protected IEnumerator DashRoutine()
    {
        _isDashing = true;
        _dashOnCooldown = true;
        
        _collider.enabled = false;

        float dashDir = Mathf.Sign(_movement.x);

        Vector3 dashVelocity = new Vector3(dashDir * _dashForce, 0f, 0f);
        Vector3 targetDashVelocity = Vector3.right * (_flySpeed * dashDir);
        _rb.velocity = dashVelocity;

        float angle = transform.eulerAngles.z;
        
        float elapsed = 0f;
        while (elapsed < _dashDuration)
        {
            yield return null;

            dashVelocity = Vector3.Lerp(dashVelocity, targetDashVelocity, _dashReduction * Time.deltaTime);
            _rb.velocity = dashVelocity;
            
            angle = Mathf.Lerp(angle, 360f + _rb.velocity.x * _tilt, _dashReduction * Time.deltaTime);
            _rb.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, -angle * dashDir);
            elapsed += Time.deltaTime;
        }
        
        _isDashing = false;
        
        _collider.enabled = true;

        yield return new WaitForSeconds(_dashCooldown);
        _dashOnCooldown = false;
    }
}
