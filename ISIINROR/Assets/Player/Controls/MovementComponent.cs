using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour
{
    private Vector2 _movement;

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private float _hangTime = 0.1f;
    [SerializeField, Range(5f, 50f)] private float _hightFallVelocity = 40f;
    [SerializeField, Range(5f, 100f)] private float _yVelocityLimit = 50f;
    [SerializeField] private GroundChecker _groundChecker;

    private float _hangTimeCounter = 0f;
    private bool _secondJump = true;
    private float _lastFrameVelocityY = 0;

    private bool _isLookRight = true;
    public bool IsLookRight { get { return _isLookRight; } }

    private float _interaptionTimer = 0;

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
        if (IsInterrupted())
        {
            _interaptionTimer -= Time.deltaTime;
            _movement.x = 0;
        }
        else
        {
            _movement.x = Input.GetAxis("Horizontal");
        }

        CheckJumpParameters();

        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        //Движение
        _rb.velocity = new Vector2(_movement.x * _playerService.Stats.MoveSpeed.Value,
                                    Mathf.Clamp(_rb.velocity.y, -_yVelocityLimit, _yVelocityLimit));

    }

    private void LateUpdate()
    {
        if (IsInterrupted()) return;

        if (Input.GetButtonDown("Jump"))
        {
            TryJump();
        }

        if ((_isLookRight && _movement.x < 0) || (!_isLookRight && _movement.x > 0))
        {
            Flip();
        }
    }

    private bool IsInterrupted()
    {
        return _interaptionTimer > 0;
    }

    public void InterruptVelocity(float interaptionTimer)
    {
        _rb.velocity = Vector2.zero;
        _interaptionTimer = interaptionTimer;
    }

    private void CheckJumpParameters()
    {
        if(_groundChecker._isGrounded && _lastFrameVelocityY <= -_hightFallVelocity && _rb.velocity.y > -_hightFallVelocity)
        {
            _playerService.InvokeOnHightFall();
            _playerService.Animator.SetHightFall();
        }
        _lastFrameVelocityY = _rb.velocity.y;

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
        _isLookRight = !_isLookRight;
        _sprite.flipX = !_isLookRight;
    }

    private void UpdateAnimations()
    {
        PlayerAnimator animator = _playerService.Animator;

        animator.SetGround(_groundChecker._isGrounded);
        animator.SetMove(_movement.x != 0);
        animator.SetVerticalVelocity(_rb.velocity.y);
    }
}
