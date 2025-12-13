using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour
{
    [Range(0.5f, 10f)] public float Radius = 0.5f;
    [HideInInspector] public WaypointConnection ConnectionComponent;

    public Vector3 GetPointPosition()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        Vector3 randomPoint = transform.right * randomX + transform.forward * randomZ;
        randomPoint.y = 0f;
        randomPoint = randomPoint.normalized * Radius + transform.position;

        return randomPoint;
    }
}
