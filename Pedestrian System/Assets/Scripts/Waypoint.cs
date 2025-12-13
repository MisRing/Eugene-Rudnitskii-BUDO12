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
        Vector3 randomPoint = Vector3.right * randomX + Vector3.forward * randomZ;
        randomPoint = randomPoint.normalized * Radius + transform.position;
        randomPoint.y = 0f;

        return randomPoint;
    }
}
