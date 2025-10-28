using Unity.VisualScripting;
using UnityEngine;

namespace Characters.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovementComponent : MonoBehaviour
    {
        private Vector2 _movement;

        [Header("Jump")]
        [SerializeField] private float _jumpForce = 3f;
        [SerializeField] private float _hangTime = 0.1f;
        [SerializeField] private float _groundJumpTimer = 0.05f;
        [SerializeField, Range(5f, 50f)] private float _highFallVelocity = 40f;
        [SerializeField, Range(5f, 100f)] private float _yVelocityLimit = 50f;

        [Header("Ground check")]
        [SerializeField] private Vector3 _groundCheckPos;
        [SerializeField] private Vector2 _direction;
        [SerializeField] private float _rayDistance = 0.15f;
        [SerializeField] private LayerMask _groundLayers;
        [SerializeField] private bool _isGrounded;

        private int _jumpCount = 0;
        private float _groundJumpTimerCounter = 0;
        private float _hangTimeCounter = 0f;
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
            GroundCheck();
            
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
            //��������
            _rb.velocity = new Vector2(
                _movement.x * _playerService.Stats.MoveSpeed.Value,
                Mathf.Clamp(_rb.velocity.y, -_yVelocityLimit, _yVelocityLimit)
                );

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
        
        private void GroundCheck()
        {
            _isGrounded = Physics2D.Raycast(transform.position + _groundCheckPos, _direction, _rayDistance, _groundLayers);
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
            if (_isGrounded && _lastFrameVelocityY <= -_highFallVelocity &&
                _rb.velocity.y > -_highFallVelocity)
            {
                _playerService.InvokeOnHighFall();
                _playerService.Animator.SetHighFall();
            }
            _lastFrameVelocityY = _rb.velocity.y;

            _groundJumpTimerCounter -= Time.deltaTime;

            if (_isGrounded)
            {
                _hangTimeCounter = _hangTime;

                if (_groundJumpTimerCounter <= 0)
                {
                    _jumpCount = 0;
                }
            }
            else
            {
                _hangTimeCounter -= Time.deltaTime;
            }
        }

        private void TryJump()
        {
            if (_jumpCount >= _playerService.Stats.Jumps.Value) return;

            if (_hangTimeCounter > 0 && _jumpCount == 0)
            {
                Jump();
            }
            else
            {
                Jump(true);
            }

            _groundJumpTimerCounter = _groundJumpTimer;
            _jumpCount++;
        }

        private void Jump(bool isDoubleJump = false)
        {
            _hangTimeCounter = 0f;

            if (isDoubleJump)
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

            animator.SetGround(_isGrounded);
            animator.SetMove(_movement.x != 0);
            animator.SetVerticalVelocity(_rb.velocity.y);
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _isGrounded ? Color.green : Color.red;
            Gizmos.DrawRay(transform.position + _groundCheckPos, _direction * _rayDistance);
        }
    }
}
