using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class WaypointConnection : MonoBehaviour
{
    [Range(0f, 1f)] public float Priority = 0.5f;
    public List<Waypoint> ConnectedWaypoints = new List<Waypoint>();

    public Waypoint GetNextWaypoint(Waypoint fromPoint)
    {
        if (ConnectedWaypoints.Count == 0)
        {
            return null;
        }

        float sum = ConnectedWaypoints.Sum(i => i.ConnectionComponent.Priority);
        if(fromPoint)
        {
            sum -= fromPoint.ConnectionComponent.Priority;
        }

        float random = Random.Range(0f, sum);

        Waypoint choosedPoint = null;

        foreach (Waypoint wp in ConnectedWaypoints)
        {
            if (wp == fromPoint) continue;

            random -= wp.ConnectionComponent.Priority;

            if (random <= 0)
            {
                choosedPoint = wp;
                break;
            }
        }

        return choosedPoint;
    }

    private void OnDestroy()
    {
        Waypoint thisPoint = GetComponent<Waypoint>();
        foreach (Waypoint point in ConnectedWaypoints)
        {
            point.ConnectionComponent.ConnectedWaypoints.Remove(thisPoint);
        }
    }
}
