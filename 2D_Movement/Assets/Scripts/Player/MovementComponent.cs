using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed = 1;
    private Vector2 _movement;

    [Header("Jump")]
    [SerializeField] private bool _isGrounded = false;
    [SerializeField] private bool _isOnWallLeft = false, _isOnWallRight = false;
    [SerializeField] private float _jumpForce = 3f, _wallJumpForce = 3f;
    [SerializeField] private float _gravity = 5f, _onWallGravity = 0.75f;
    [SerializeField] private bool _secondJump = true;
    [SerializeField] private Transform _groundTarget;
    [SerializeField] private Transform _wallTargetLeft, _wallTargetRight;
    [SerializeField] private float _rayDistance = 0.15f;
    [SerializeField] private LayerMask _jumpLayers;
    private Vector2 _jumpVelocity;


    [Header("Face")]
    [SerializeField] private bool _lookRight = true;
    [SerializeField] private SpriteRenderer _sprite;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        _movement.x = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        _jumpVelocity.x = Mathf.Sign(_jumpVelocity.x) * Mathf.Clamp(Mathf.Abs(_jumpVelocity.x) - Time.deltaTime * 10, 0, float.MaxValue);

        _rb.velocity = new Vector2((_movement.x + _jumpVelocity.x) * _speed, _rb.velocity.y);

        _isGrounded = Physics2D.Raycast(_groundTarget.position, Vector2.down, _rayDistance, _jumpLayers);
        _isOnWallLeft = Physics2D.Raycast(_wallTargetLeft.position, Vector2.left, _rayDistance, _jumpLayers);
        _isOnWallRight = Physics2D.Raycast(_wallTargetRight.position, Vector2.right, _rayDistance, _jumpLayers);

        if (((_isOnWallLeft && Input.GetAxisRaw("Horizontal") < 0)|| (_isOnWallRight && Input.GetAxisRaw("Horizontal") > 0))
            && _rb.velocity.y <= 0)
        {
            _rb.gravityScale = _onWallGravity;
        }
        else
        {
            _rb.gravityScale = _gravity;
        }

        if (_isGrounded || _isOnWallLeft || _isOnWallRight)
        {
            _secondJump = true;
        }
    }

    private void LateUpdate()
    {
        if ((_lookRight && _movement.x < 0) || (!_lookRight && _movement.x > 0))
        {
            Flip();
        }


        if (Input.GetButtonDown("Jump"))
        {
            if (_isGrounded)
            {
                Jump();
            }
            else if(_isOnWallLeft)
            {
                Jump(Vector2.up + Vector2.right);
            }
            else if(_isOnWallRight)
            {
                Jump(Vector2.up + Vector2.left);
            }
            else if (_secondJump)
            {
                _secondJump = false;
                Jump();
            }
        }
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
    }

    private void Jump(Vector2 _direction)
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _direction.y * _jumpForce);
        _jumpVelocity.x = _direction.x * _wallJumpForce;
    }

    private void Flip()
    {
        _lookRight = !_lookRight;
        _sprite.flipX = !_lookRight;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Gizmos.DrawRay(_groundTarget.position, Vector2.down * _rayDistance);

        Gizmos.color = _isOnWallLeft ? Color.green : Color.red;
        Gizmos.DrawRay(_wallTargetLeft.position, Vector2.left * _rayDistance);

        Gizmos.color = _isOnWallRight ? Color.green : Color.red;
        Gizmos.DrawRay(_wallTargetRight.position, Vector2.right * _rayDistance);
    }
}
