using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour
{
    public Waypoint PrevPoint, NextPoint;
    [Range(0.5f, 10f)] public float Radius = 0.5f;
    public int Priority = 5;

    public List<Waypoint> Branches;

    public Waypoint GetNextWaypoint(ref bool isMoovingForvard, Waypoint fromPoint)
    {
        List<Waypoint> points = new List<Waypoint>();
        if(Branches != null)
        {
            points.AddRange(Branches);
        }
        if (isMoovingForvard && NextPoint)
        {
            points.Add(NextPoint);
        }
        if (!isMoovingForvard && PrevPoint)
        {
            points.Add(PrevPoint);
        }

        if(fromPoint)
        {
            points.Remove(fromPoint);
        }

        if(points.Count == 0)
        {
            return null;
        }

        int summ = points.Sum(i => i.Priority);

        int random = Random.Range(0, summ);

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
        if(choosedPoint == null)
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
        if (Branches != null)
        {
            foreach (Waypoint wp in Branches)
            {
                wp.Branches.Remove(this);
            }
        }

        if (PrevPoint && NextPoint)
        {
            PrevPoint.NextPoint = NextPoint;
            NextPoint.PrevPoint = PrevPoint;
        }
        else if(NextPoint)
        {
            NextPoint.PrevPoint = null;
        }
        else if (PrevPoint)
        {
            PrevPoint.NextPoint = null;
        }
    }
}
