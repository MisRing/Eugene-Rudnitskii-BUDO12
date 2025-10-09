using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed = 1;
    private Vector2 _movement;

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private float _wallJumpForce = 3f;
    [SerializeField] private float _jumpAffectSpeed = 10f;
    [SerializeField] private float _gravity = 5f, _onWallGravity = 0.75f;
    [SerializeField] private bool _secondJump = true;
    [SerializeField] private GroundChecker _groundChecker, _leftWallChecker, _rightWallChecher;

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
        _jumpVelocity.x = AffectToZero(_jumpVelocity.x, _jumpAffectSpeed);

        _rb.velocity = new Vector2((_movement.x + _jumpVelocity.x) * _speed, _rb.velocity.y);

        //_rb.velocity = new Vector2((_movement.x) * _speed, _rb.velocity.y);

        CheckGravity();

        CheckSecondJump();
    }

    private void LateUpdate()
    {
        if ((_lookRight && _movement.x < 0) || (!_lookRight && _movement.x > 0))
        {
            Flip();
        }

        if (Input.GetButtonDown("Jump"))
        {
            TryJump();
        }
    }

    private void CheckGravity()
    {
        if (((_leftWallChecker._isGrounded && _movement.x < 0) || (_rightWallChecher._isGrounded && _movement.x > 0)) && _rb.velocity.y <= 0)
        {
            _rb.gravityScale = _onWallGravity;
        }
        else
        {
            _rb.gravityScale = _gravity;
        }
    }

    private void CheckSecondJump()
    {
        if (_groundChecker._isGrounded || _leftWallChecker._isGrounded || _rightWallChecher._isGrounded)
        {
            _secondJump = true;
        }
    }

    private void TryJump()
    {
        if (_groundChecker._isGrounded)
        {
            Jump();
        }
        else if (_leftWallChecker._isGrounded)
        {
            Jump(Vector2.up + Vector2.right);
        }
        else if (_rightWallChecher._isGrounded)
        {
            Jump(Vector2.up + Vector2.left);
        }
        else if (_secondJump)
        {
            _secondJump = false;
            Jump();
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

    //private void Jump(Vector2 _direction)
    //{
    //    _rb.velocity = new Vector2(_direction.x * _wallJumpForce, _direction.y * _jumpForce);
    //}

    private void Flip()
    {
        _lookRight = !_lookRight;
        _sprite.flipX = !_lookRight;
    }

    private float AffectToZero(float _value, float _affectSpeed)
    {
        return Mathf.Sign(_value) * Mathf.Clamp(Mathf.Abs(_value) - _affectSpeed * Time.deltaTime, 0, float.MaxValue);
    }
}
