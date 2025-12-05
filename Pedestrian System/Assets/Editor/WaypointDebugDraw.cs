using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class WaypointDebugDraw
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawGizmo(Waypoint point, GizmoType gizmoType)
    {
        float visualMod = 1f;
        if((gizmoType & GizmoType.Selected) == 0)
        {
            visualMod = 0.85f;
        }

        Gizmos.color = Color.darkRed * visualMod;
        Gizmos.DrawSphere(point.transform.position, 0.2f * visualMod);

        Handles.color = Color.white * visualMod;
        Handles.DrawWireDisc(point.transform.position, Vector3.up, point.Radius, 5f * visualMod);

        Handles.color = Color.orangeRed * visualMod;
        Handles.DrawLine(   point.transform.right * point.Radius + point.transform.position,
                            -point.transform.right * point.Radius + point.transform.position,
                            2f * visualMod);

        Handles.color = Color.yellow * visualMod;

        Vector3 offset = point.transform.position + point.transform.forward * 0.35f;

        Handles.DrawLine(   point.transform.right * 0.25f + offset,
                            point.transform.forward * 0.35f + offset,
                            5f * visualMod);
        Handles.DrawLine(   -point.transform.right * 0.25f + offset,
                            point.transform.forward * 0.35f + offset,
                            5f * visualMod);

        offset = point.transform.position - point.transform.forward * 0.65f;

        Handles.DrawLine(point.transform.right * 0.25f + offset,
                            point.transform.forward * 0.35f + offset,
                            5f * visualMod);
        Handles.DrawLine(-point.transform.right * 0.25f + offset,
                            point.transform.forward * 0.35f + offset,
                            5f * visualMod);

        Handles.color = Color.blue * visualMod;
        foreach (Waypoint cPoint in point.ConnectedWaypoints)
        {
            Handles.DrawLine(   point.transform.position,
                cPoint.transform.position,
                3f * visualMod);
        }
    }
}
