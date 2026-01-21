using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnitMovement : MonoBehaviour
{
    [HideInInspector] public Unit UnitController;
    [HideInInspector] public Vector3 MovementDirection = Vector3.zero;

    [SerializeField] private float _endThreshold = 0.1f;
    private Rigidbody _rb;

    [SerializeField] private Queue<Vector3> _movementQs = new Queue<Vector3>();

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_movementQs.Count == 0) return;

        Vector3 direction = _movementQs.Peek() - transform.position;
        direction.y = 0;
        direction = direction.normalized * Mathf.Clamp(direction.magnitude * 3, 1.8f, 3f) / 3f;

        _rb.linearVelocity = direction * UnitController.Stats.MoveSpeed * Time.fixedDeltaTime;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, UnitController.Stats.RotationSpeed * Time.fixedDeltaTime);
        }

        MovementDirection = transform.InverseTransformDirection(direction);

        if (Vector3.Distance(transform.position, _movementQs.Peek()) <= _endThreshold)
        {
            _movementQs.Dequeue();
            MovementDirection = Vector3.zero;
            _rb.linearVelocity = Vector3.zero;
        }
    }

    public void AddOrder(Vector3 point, bool addToQ)
    {
        if(!addToQ)
        {
            _movementQs.Clear();
        }

        _movementQs.Enqueue(point);
    }
}
