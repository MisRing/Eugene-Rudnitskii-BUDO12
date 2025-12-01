using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour
{
    public Waypoint PrevPoint, NextPoint;
    [Range(0.5f, 10f)] public float Radius = 0.5f;

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
