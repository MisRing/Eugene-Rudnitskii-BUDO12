using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour
{
    private Vector2 _movement;

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private GroundChecker _groundChecker;
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

        CheckSecondJump();

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

    private void CheckSecondJump()
    {
        if (_groundChecker._isGrounded)
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
        else if (_secondJump)
        {
            _secondJump = false;
            Jump(true);
        }
    }

    private void Jump(bool isDoubleJump = false)
    {
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
