using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class CameraDebug
{
    [Header("Debug settings")]
    private static Color LINE_COLOR = Color.white;
    private static Color VIEW_COLOR = Color.lightYellow;
    private static float LINE_THICKNESS = 3f;
    private static float POSITION_RADIUS = 0.75f;


    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawGizmo(CameraControl cameraControl, GizmoType gizmoType)
    {
        Vector3 cameraPos = cameraControl.transform.position;
        Vector3 cameraGroundedPos = new Vector3(cameraPos.x, 0f, cameraPos.z);

        Handles.color = LINE_COLOR;
        Handles.DrawLine(cameraPos, cameraGroundedPos, LINE_THICKNESS * 0.5f);
        Handles.DrawWireDisc(cameraGroundedPos, Vector3.up, POSITION_RADIUS, LINE_THICKNESS * 0.5f);

        Camera cam = Camera.main;

        Vector3 leftBot = GetGroundPoint(cam, new Vector2(0f, 0f));
        Vector3 rightBot = GetGroundPoint(cam, new Vector2(1f, 0f));
        Vector3 rightTop = GetGroundPoint(cam, new Vector2(1f, 1f));
        Vector3 leftTop = GetGroundPoint(cam, new Vector2(0f, 1f));

        Handles.color = VIEW_COLOR;

        Handles.DrawLine(leftBot, rightBot, LINE_THICKNESS);
        Handles.DrawLine(rightBot, rightTop, LINE_THICKNESS);
        Handles.DrawLine(rightTop, leftTop, LINE_THICKNESS);
        Handles.DrawLine(leftTop, leftBot, LINE_THICKNESS);
    }

    static Vector3 GetGroundPoint(Camera cam, Vector2 viewportPos)
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(viewportPos.x, viewportPos.y, 0f));

        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float enter))
        {
            return ray.GetPoint(enter);
        }

        return Vector3.zero;
    }
}
