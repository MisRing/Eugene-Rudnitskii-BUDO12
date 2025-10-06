using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed = 1;
    private Vector2 _movement;

    [Header("Face")]
    [SerializeField] private bool _bIsRight = true;
    [SerializeField] private SpriteRenderer _sprite;

    private Rigidbody2D _rb;


    private void Awake()
    {
        if (_rb == null)
            _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _movement.x = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_movement.x * _speed, _rb.velocity.y);
    }

    private void LateUpdate()
    {
        if ((_bIsRight && _movement.x < 0) || (!_bIsRight && _movement.x > 0))
        {
            Flip();
        }
    }

    private void Flip()
    {
        _bIsRight = !_bIsRight;
        _sprite.flipX = !_bIsRight;

    }
}
