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
        Gizmos.DrawSphere(point.transform.position, 0.08f * visualMod);

        Handles.color = Color.white * visualMod;
        Handles.DrawWireDisc(point.transform.position, Vector3.up, point.Radius, 3f * visualMod);
        Handles.DrawLine(   point.transform.right * point.Radius + point.transform.position,
                            -point.transform.right * point.Radius + point.transform.position,
                            3f * visualMod);

        Handles.DrawLine(   point.transform.right * 0.11f + point.transform.position,
                            point.transform.forward * 0.2f + point.transform.position,
                            2f * visualMod);
        Handles.DrawLine(   -point.transform.right * 0.11f + point.transform.position,
                            point.transform.forward * 0.2f + point.transform.position,
                            2f * visualMod);

        if (point.NextPoint != null)
        {
            if(Selection.activeObject == point.NextPoint.gameObject)
            {
                visualMod = 1f;
            }

            Handles.color = Color.green * visualMod;
            Handles.DrawLine(point.transform.right * point.Radius + point.transform.position,
                            point.NextPoint.transform.right * point.NextPoint.Radius + point.NextPoint.transform.position,
                            3f * visualMod);

            Handles.color = Color.red * visualMod;
            Handles.DrawLine(-point.transform.right * point.Radius + point.transform.position,
                            -point.NextPoint.transform.right * point.NextPoint.Radius + point.NextPoint.transform.position,
                            3f * visualMod);
        }
    }
}
