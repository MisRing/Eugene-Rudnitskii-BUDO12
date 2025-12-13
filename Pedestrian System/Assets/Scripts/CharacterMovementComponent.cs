using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovementComponent : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _rotationSpeed = 1f;
    public Vector3 TargetPosition;
    private bool _isMoving = false;
    public bool IsMoving
    {
        get { return _isMoving; }
        set
        {
            if(!value && _rb)
            {
                _rb.linearVelocity = Vector3.zero;
            }
            _isMoving = value;
        }
    }

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Initialize(float speed)
    {
        _speed = speed;
    }

    private void FixedUpdate()
    {
        if (!_isMoving) return;

        Vector3 movementVelocity = transform.forward * _speed;
        _rb.linearVelocity = new Vector3(movementVelocity.x, _rb.linearVelocity.y, movementVelocity.z);

        Vector3 direction = (TargetPosition - new Vector3(transform.position.x, 0f, transform.position.z));

        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);

        _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime));
    }
}
