using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _rotationSpeed = 1f;
    public Vector3 TargetPosition;
    public bool IsMoving = false;

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
        if (!IsMoving) return;

        _rb.linearVelocity = transform.forward * _speed;

        Vector3 direction = (TargetPosition - new Vector3(transform.position.x, 0f, transform.position.z));

        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);

        _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime));
    }
}
