using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _rayDistance = 0.15f;
    [SerializeField] private LayerMask _groundLayers;

    public bool _isGrounded { get; private set; }

    private void Update()
    {
        _isGrounded = Physics2D.Raycast(transform.position, _direction, _rayDistance, _groundLayers);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position, _direction * _rayDistance);
    }
}
