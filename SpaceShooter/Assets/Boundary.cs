using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    [SerializeField] private float minX, maxX, minZ, maxZ;

    private BoxCollider _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;
    }

    public Vector3 LimitObject(Vector3 position)
    {
        Vector3 currentPos = transform.position;

        Vector3 newPosition = new Vector3(  Mathf.Clamp(position.x, currentPos.x + minX, currentPos.x + maxX),
                                            position.y,
                                            Mathf.Clamp(position.z, currentPos.z + minZ, currentPos.z + maxZ));

        return newPosition;
    }
}
