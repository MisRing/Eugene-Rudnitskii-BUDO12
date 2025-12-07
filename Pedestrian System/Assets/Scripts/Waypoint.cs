using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour
{
    public List<Waypoint> ConnectedWaypoints = new List<Waypoint>();
    [Range(0.5f, 10f)] public float Radius = 0.5f;
    [Range(0f, 1f)] public float Priority = 0.5f;
    
    public Waypoint GetNextWaypoint(Waypoint fromPoint)
    {
        List<Waypoint> points = ConnectedWaypoints.ToList();
        
        if(fromPoint)
        {
            points.Remove(fromPoint);
        }

        if(points.Count == 0)
        {
            return null;
        }

        float sum = points.Sum(i => i.Priority);

        float random = Random.Range(0f, sum);

        Waypoint choosedPoint = null;

        foreach (Waypoint wp in points)
        {
            random -= wp.Priority;

            if (random <= 0)
            {
                choosedPoint = wp;
                break;
            }
        }
        if(!choosedPoint)
        {
            choosedPoint = points[points.Count - 1];
        }

        return choosedPoint;
    }

    public Vector3 GetPoint()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        Vector3 randomPoint = transform.right * randomX + transform.forward * randomZ;
        randomPoint.y = 0f;
        randomPoint = randomPoint.normalized * Radius + transform.position;

        return randomPoint;
    }

    private void OnDestroy()
    {
        foreach (Waypoint point in ConnectedWaypoints)
        {
            point.ConnectedWaypoints.Remove(this);
        }
    }
}
