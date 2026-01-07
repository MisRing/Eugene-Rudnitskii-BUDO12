using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnitMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
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
        _rb.linearVelocity = direction.normalized * _speed * Time.fixedDeltaTime;

        if(Vector3.Distance(transform.position, _movementQs.Peek()) <= _endThreshold)
        {
            _movementQs.Dequeue();
        }
    }

    public void AddOrder(Vector3 point, bool newQueue)
    {
        if(newQueue)
        {
            _movementQs.Clear();
        }

        _movementQs.Enqueue(point);
    }
}
