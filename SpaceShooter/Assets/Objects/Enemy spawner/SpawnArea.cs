using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [Header("Spawn bounds")]
    [SerializeField] private Vector2 _size;

    [Header("Gizmos")]
    [SerializeField] private bool _drawGizmos = true;

    public Vector3 GetRandomPosition()
    {
        Vector3 randPos = transform.position;

        randPos.x += Random.Range(-_size.x / 2, _size.x / 2);
        randPos.z += Random.Range(-_size.y / 2, _size.y / 2);

        return randPos;
    }

    private void OnDrawGizmos()
    {
        if (!_drawGizmos) return;

        Gizmos.color = new Color(0.627f, 0.125f, 0.941f, 1f);

        Gizmos.DrawWireCube(transform.position, new Vector3(_size.x, 5f, _size.y));
    }
}
