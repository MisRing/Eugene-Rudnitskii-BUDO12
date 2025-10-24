using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour
{
    private Vector2 _movement;

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private float _hangTime = 0.1f;
    [SerializeField] private GroundChecker _groundChecker;

    private float _hangTimeCounter = 0f;
    private bool _secondJump = true;

    private bool _lookRight = true;

    private SpriteRenderer _sprite;
    private Rigidbody2D _rb;
    private PlayerService _playerService;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _sprite = GetComponent<SpriteRenderer>();
        _playerService = GetComponent<PlayerService>();
    }

    private void Update()
    {
        _movement.x = Input.GetAxis("Horizontal");

        CheckJumpParameters();

        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        //Движение
        _rb.velocity = new Vector2(_movement.x * _playerService.Stats.MoveSpeed.Value, _rb.velocity.y);
    }

    private void LateUpdate()
    {
        if (Input.GetButtonDown("Jump"))
        {
            TryJump();
        }

        //if(Input.GetMouseButtonDown(1))
        //{
        //    _playerService.Animator.SetHit();
        //}

        if ((_lookRight && _movement.x < 0) || (!_lookRight && _movement.x > 0))
        {
            Flip();
        }
    }

    private void CheckJumpParameters()
    {
        if (_groundChecker._isGrounded)
        {
            _secondJump = true;
            _hangTimeCounter = _hangTime;
        }
        else
        {
            _hangTimeCounter -= Time.deltaTime;
        }
    }

    private void TryJump()
    {
        if (_hangTimeCounter > 0)
        {
            Jump();
        }
        else if (_secondJump)
        {
            _secondJump = false;
            Jump(true);
        }
    }

    private void Jump(bool isDoubleJump = false)
    {
        _hangTimeCounter = 0f;

        if(isDoubleJump)
        {
            _playerService.Animator.SetDoubleJump();
        }
        else
        {
            _playerService.Animator.SetJump();
        }

        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
    }

    private void Flip()
    {
        _lookRight = !_lookRight;
        _sprite.flipX = !_lookRight;
    }

    private void UpdateAnimations()
    {
        PlayerAnimator animator = _playerService.Animator;

        animator.SetGround(_groundChecker._isGrounded);
        animator.SetMove(_movement.x != 0);
        animator.SetVerticalVelocity(_rb.velocity.y);
    }
}
