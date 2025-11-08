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

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;
    }

    public Vector3 LimitObject(Vector3 position)
    {
        Vector3 currentPos = transform.position;

        Vector3 newPosition = new Vector3(  Mathf.Clamp(position.x, currentPos.x + _minX, currentPos.x + _maxX),
                                            position.y,
                                            Mathf.Clamp(position.z, currentPos.z + _minZ, currentPos.z + _maxZ));

        return newPosition;
    }

    private void OnTriggerExit(Collider collision)
    {
        IReturnable obj = collision.gameObject.GetComponent<IReturnable>();
        if (obj != null)
        {
            obj.ReturnThis();
        }
    }

    private void OnDrawGizmos()
    {
        if (!_drawGizmos) return;

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(transform.position, new Vector3(_maxX - _minX, 5f, _maxZ - _minZ));
    }
}
