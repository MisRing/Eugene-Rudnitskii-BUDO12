using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    [Header("Bounds")]
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minZ;
    [SerializeField] private float _maxZ;

    [Header("Gizmos")]
    [SerializeField] private bool _drawGizmos = true;

    private BoxCollider _boxCollider;
    
    public float MinX { get { return _minX + transform.position.x; } }
    public float MaxX { get { return _maxX + transform.position.x; } }
    public float MinZ { get { return _minZ + transform.position.z; } }
    public float MaxZ { get { return _maxZ + transform.position.z; } }


    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;
    }

    private void OnDrawGizmos()
    {
        if (!_drawGizmos) return;

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(transform.position, new Vector3(_maxX - _minX, 5f, _maxZ - _minZ));
    }
}
