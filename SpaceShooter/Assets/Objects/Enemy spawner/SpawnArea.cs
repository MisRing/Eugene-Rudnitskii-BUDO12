using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [Header("Spawn bounds")]
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minZ;
    [SerializeField] private float _maxZ;

    [Header("Gizmos")]
    [SerializeField] private bool _drawGizmos = true;

    public Vector3 GetRandomPosition()
    {
        Vector3 randPos = transform.position;

        randPos.x += Random.Range(_minX, _maxX);
        randPos.z += Random.Range(_minZ, _maxZ);

        return randPos;
    }

    private void OnDrawGizmos()
    {
        if (!_drawGizmos) return;

        Gizmos.color = new Color(0.627f, 0.125f, 0.941f, 1f);

        Gizmos.DrawWireCube(transform.position, new Vector3(_maxX - _minX, 5f, _maxZ - _minZ));
    }
}
