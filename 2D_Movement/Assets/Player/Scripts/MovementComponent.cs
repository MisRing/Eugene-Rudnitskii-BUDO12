using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed = 1;
    [HideInInspector] public float Speed { get => _speed;}
    private Vector2 _movement;

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private float _wallJumpForce = 3f;
    [SerializeField] private float _slideSpeed = 1f;
    [SerializeField] private GroundChecker _groundChecker, _leftWallChecker, _rightWallChecker;
    private bool _secondJump = true;
    private bool _onWall = false;

    //[Header("Face")]
    private bool _lookRight = true;
    private SpriteRenderer _sprite;

    private Rigidbody2D _rb;
    private AnimatorComponent _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<AnimatorComponent>();
    }

    private void Update()
    {
        _movement.x = Input.GetAxis("Horizontal");

        CheckGravity();

        CheckSecondJump();

        SetAnimations();
    }

    private void FixedUpdate()
    {
        //Движение
        _rb.velocity = new Vector2(_movement.x * _speed, _rb.velocity.y);
    }

    private void LateUpdate()
    {
        if (Input.GetButtonDown("Jump"))
        {
            TryJump();
        }

        if (_onWall)
        {
            //Взгляд от стены при зацеплении на ней
            if ((_lookRight && _rightWallChecker._isGrounded) || (!_lookRight && _leftWallChecker._isGrounded))
            {
                Flip();
            }
        }
        else if ((_lookRight && _movement.x < 0) || (!_lookRight && _movement.x > 0))
        {
            Flip();
        }
    }

    private void CheckGravity()
    {
        if ((_leftWallChecker._isGrounded || _rightWallChecker._isGrounded) && !_groundChecker._isGrounded)
        {
            // Скольжение по стене с лимитом скорости
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Clamp(_rb.velocity.y, -1f * _slideSpeed * Time.fixedDeltaTime, float.MaxValue));
            _onWall = true;
        }
        else
        {
            _onWall = false;
        }
    }

    private void CheckSecondJump()
    {
        if (_groundChecker._isGrounded || _leftWallChecker._isGrounded || _rightWallChecker._isGrounded)
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
            WallJump(Vector2.up * 2 + Vector2.right);
        }
        else if (_rightWallChecker._isGrounded)
        {
            WallJump(Vector2.up * 2 + Vector2.left);
        }
        else if (_secondJump)
        {
            _secondJump = false;
            Jump();
        }
    }

    private void Jump()
    {
        _animator.StartJump();
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
    }

    private void WallJump(Vector2 _direction)
    {
        _animator.StartJump();
        _rb.velocity = Vector2.zero;
        _rb.AddForce(_direction * _wallJumpForce, ForceMode2D.Impulse);
    }

    private void Flip()
    {
        _lookRight = !_lookRight;
        _sprite.flipX = !_lookRight;
    }

    private void SetAnimations()
    {
        _animator.SetWalkState(_movement.x);
        _animator.SetVerticalVelocity(_rb.velocity.y);
        _animator.SetGroundState(_groundChecker._isGrounded);
    }
}
