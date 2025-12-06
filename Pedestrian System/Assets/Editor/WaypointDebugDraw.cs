using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class WaypointDebugDraw
{
    [Header("Point settings")]
    private static float POINT_SIZE = 0.3f;
    private static float RADIUS_THICKNESS = 5f;

    [Header("Arrows settings")]
    private static float ARROW_OFFSET = 0.45f;
    private static float ARROW_CENTER_OFFSET = 0.15f;
    private static float ARROW_SIZE = 0.5f;
    private static float ARROW_THICKNESS = 3f;

    [Header("Way settings")]
    private static float WAY_THICKNESS_MIN = 1f;
    private static float WAY_THICKNESS_MAX = 5f;

    [Header("Text settings")]
    private static float TEXT_SIZE = 16f;

    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawGizmo(Waypoint point, GizmoType gizmoType)
    {
        float visualMod = 1f;
        bool isSelected = (gizmoType & GizmoType.Selected) != 0;
        Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;

        if (!isSelected)
        {
            visualMod = 0.95f;
        }

        Color pointColor = Color.darkBlue;
        if (isSelected)
        {
            pointColor = Color.darkGreen;
        }

        DrawPointMarker(point, visualMod, pointColor);
        DrawPointRadius(point, visualMod, pointColor);
        DrawPointDirection(point, visualMod, Color.black, 2, Color.darkRed);

        Color wayColor = Color.darkOrange;
        if (isSelected)
        {
            wayColor = Color.darkGreen;
        }

        foreach (Waypoint cPoint in point.ConnectedWaypoints)
        {
            DrawWay(point, cPoint, visualMod, wayColor, isSelected, 1, Color.darkRed);
        }
    }

    private static void DrawPointMarker(Waypoint point, float visualMod, Color color)
    {
        Gizmos.color = color * visualMod;

        Gizmos.DrawSphere(point.transform.position, POINT_SIZE * visualMod);
    }

    private static void DrawPointRadius(Waypoint point, float visualMod, Color color)
    {
        Handles.color = color * visualMod;

        Handles.DrawWireDisc(point.transform.position, Vector3.up, point.Radius, RADIUS_THICKNESS * visualMod);
    }

    private static void DrawPointDirection(Waypoint point, float visualMod, Color diameterColor, int arrowCount, Color arrowColor)
    {
        Handles.color = diameterColor * visualMod;

        Vector3 rightDot = point.transform.right * point.Radius;
        Handles.DrawLine(   rightDot + point.transform.position,
                            -rightDot + point.transform.position,
                            RADIUS_THICKNESS * visualMod);

        for(int i = -arrowCount; i < arrowCount; i++)
        {
            float arrowCenterOffset = ARROW_CENTER_OFFSET * Mathf.Sign(i);
            Vector3 arrowCenter = point.transform.forward * (ARROW_OFFSET * i + arrowCenterOffset) + point.transform.position;

            DrawDirectionArrow(arrowCenter, point.transform.forward, visualMod, arrowColor);
        }
    }

    private static void DrawWay(Waypoint fromP, Waypoint toP, float visualMod, Color color, bool isSelected, int arrowCount, Color arrowColor)
    {
        Handles.color = color * visualMod;

        float wayWeight = WAY_THICKNESS_MIN + toP.Priority * (WAY_THICKNESS_MAX - WAY_THICKNESS_MIN);

        Vector3 offsetDirection = Vector3.Cross(Vector3.up, (toP.transform.position - fromP.transform.position).normalized);

        Vector3 from = fromP.transform.position + offsetDirection * fromP.Radius;
        Vector3 to = toP.transform.position + offsetDirection * toP.Radius;

        Handles.DrawLine(from, to, wayWeight * visualMod);

        GUIStyle labelStyle = new GUIStyle();
        labelStyle.fontSize = Mathf.RoundToInt(TEXT_SIZE);
        labelStyle.normal.textColor = Color.black;
        labelStyle.fontStyle = FontStyle.Bold;

        for (int i = -arrowCount; i < arrowCount; i++)
        {
            Vector3 wayCenter = (from + to) / 2f;
            Vector3 arrowDirection = (to - from).normalized;
            Vector3 arrowCenter = arrowDirection * (ARROW_OFFSET * i) + wayCenter;

            DrawDirectionArrow(arrowCenter, arrowDirection, visualMod, arrowColor, 0.75f);
            if (isSelected)
            {
                Handles.Label(wayCenter + Vector3.up * 0.1f, toP.Priority.ToString(), labelStyle);
            }
        }
    }

    private static void DrawDirectionArrow(Vector3 center, Vector3 direction, float visualMod, Color color, float sizeMod = 1f)
    {
        Handles.color = color * visualMod;

        Vector3 arrC = direction * ARROW_SIZE * sizeMod + center;
        Vector3 arrR = Vector3.Cross(Vector3.up, direction) * ARROW_SIZE * sizeMod * 0.85f + center;
        Vector3 arrL = -Vector3.Cross(Vector3.up, direction) * ARROW_SIZE * sizeMod * 0.85f + center;

        Handles.DrawLine(arrL, arrC, ARROW_THICKNESS * visualMod);
        Handles.DrawLine(arrR, arrC, ARROW_THICKNESS * visualMod);
    }
}
