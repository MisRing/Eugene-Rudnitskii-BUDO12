using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour
{
    public List<Waypoint> ConnectedWaypoints = new List<Waypoint>();
    [Range(0.5f, 10f)] public float Radius = 0.5f;
    public int Priority = 5;
    
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

        int sum = points.Sum(i => i.Priority);

        int random = Random.Range(0, sum);

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
        Vector3 leftBounds = -transform.right * Radius + transform.position;
        Vector3 rightBounds = transform.right * Radius + transform.position;

        Vector3 randomPoint = Vector3.Lerp(leftBounds, rightBounds, Random.Range(0f, 1f));
        randomPoint.y = 0f;

        return randomPoint;
    }

    private void OnDestroy()
    {
       
    }
}
