using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class CameraDebug
{
    [Header("Debug settings")]
    private static Color LINE_COLOR = Color.white;
    private static float LINE_THICKNESS = 3f;
    private static float POSITION_RADIUS = 0.75f;


    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawGizmo(CameraControl cameraControl, GizmoType gizmoType)
    {
        Vector3 cameraPos = cameraControl.transform.position;
        Vector3 cameraGroundedPos = new Vector3(cameraPos.x, 0f, cameraPos.z);

        Handles.color = LINE_COLOR;
        Handles.DrawLine(cameraPos, cameraGroundedPos, LINE_THICKNESS);
        Handles.DrawWireDisc(cameraGroundedPos, Vector3.up, POSITION_RADIUS, LINE_THICKNESS);

        Vector2 pos1 = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, cameraPos.y));
        Vector2 pos2 = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, cameraPos.y));
        Vector2 pos3 = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 10));
        Vector2 pos4 = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 10));

        Handles.color = Color.red;

        Handles.DrawLine(pos1, pos2, LINE_THICKNESS);
        Handles.DrawLine(pos2, pos3, LINE_THICKNESS);
        Handles.DrawLine(pos3, pos4, LINE_THICKNESS);
        Handles.DrawLine(pos4, pos1, LINE_THICKNESS);


    }
}
