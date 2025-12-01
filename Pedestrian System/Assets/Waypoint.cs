using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour
{
    public Waypoint PrevPoint, NextPoint;
    [Range(0.5f, 10f)] public float Radius = 0.5f;

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
        if(PrevPoint && NextPoint)
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
