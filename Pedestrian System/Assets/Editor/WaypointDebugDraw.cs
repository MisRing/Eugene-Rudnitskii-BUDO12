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
            visualMod = 0.75f;
        }

        Gizmos.color = Color.yellow * visualMod;
        Gizmos.DrawSphere(point.transform.position, 0.2f * visualMod);

        Handles.color = Color.white * visualMod;
        Handles.DrawWireDisc(point.transform.position, Vector3.up, point.Radius, 4f * visualMod);
        Handles.DrawLine(   point.transform.right * point.Radius + point.transform.position,
                            -point.transform.right * point.Radius + point.transform.position,
                            3f * visualMod);

        Handles.DrawLine(   point.transform.right * 0.25f + point.transform.position,
                            point.transform.forward * 0.45f + point.transform.position,
                            3f * visualMod);
        Handles.DrawLine(   -point.transform.right * 0.25f + point.transform.position,
                            point.transform.forward * 0.45f + point.transform.position,
                            3f * visualMod);
        
        foreach (Waypoint cPoint in point.ConnectedWaypoints)
        {
            Handles.DrawLine(   point.transform.position,
                cPoint.transform.position,
                3f * visualMod);
        }
    }
}
