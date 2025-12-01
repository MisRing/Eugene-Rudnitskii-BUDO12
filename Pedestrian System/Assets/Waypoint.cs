using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour
{
    public Waypoint PrevPoint, NextPoint;
    [SerializeField, Range(0.25f, 10f)] private float _radius = 0.5f;

    public float Radius { get { return _radius; }}

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
