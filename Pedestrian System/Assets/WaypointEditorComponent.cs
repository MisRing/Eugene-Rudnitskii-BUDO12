using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WaypointEditorComponent : EditorWindow
{
    private Transform _waypointRoot;
    private float _creatingStep = 1f;

    [MenuItem("Tools/Waypoint Editor")]
    public static void OnOpen()
    {
        GetWindow<WaypointEditorComponent>();
    }

    public void OnGUI()
    {
        _waypointRoot = (Transform)EditorGUILayout.ObjectField(
            "Waypont Root",
            _waypointRoot,
            typeof(Transform),
            true
        );

        if(_waypointRoot == null)
        {
            EditorGUILayout.HelpBox("Choose Waypoint Root!", MessageType.Warning);
            return;
        }

        _creatingStep = EditorGUILayout.FloatField("Step", _creatingStep);

        DrawButtons();
    }

    public void DrawButtons()
    {
        if (GUILayout.Button("Create Next Waypoint"))
        {
            Waypoint lastWaypoint = null;
            if (Selection.activeObject)
            {
                lastWaypoint = Selection.activeObject.GetComponent<Waypoint>();
            }
            CreateNextWaypoint(lastWaypoint);
        }

        if (GUILayout.Button("Create Previous Waypoint"))
        {
            Waypoint firstWaypoint = null;
            if (Selection.activeObject)
            {
                firstWaypoint = Selection.activeObject.GetComponent<Waypoint>();
            }
            CreatePreviousWaypoint(firstWaypoint);
        }
    }

    public void CreateNextWaypoint(Waypoint prevWaypoint)
    {
        GameObject waypointObject = new GameObject("Waypoint " + _waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(_waypointRoot, false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();

        if(prevWaypoint != null)
        {
            if(prevWaypoint.NextPoint)
            {
                waypoint.transform.position = Vector3.Lerp(prevWaypoint.transform.position, prevWaypoint.NextPoint.transform.position, 0.5f);
                waypoint.transform.rotation = Quaternion.Lerp(prevWaypoint.transform.rotation, prevWaypoint.NextPoint.transform.rotation, 0.5f);

                waypoint.NextPoint = prevWaypoint.NextPoint;
                waypoint.NextPoint.PrevPoint = waypoint;

                prevWaypoint.NextPoint = waypoint;
                waypoint.PrevPoint = prevWaypoint;
            }
            else
            {
                waypoint.transform.position = prevWaypoint.transform.forward * _creatingStep + prevWaypoint.transform.position;
                waypoint.transform.rotation = prevWaypoint.transform.rotation;

                prevWaypoint.NextPoint = waypoint;
                waypoint.PrevPoint = prevWaypoint;
            }
        }
        else if(_waypointRoot.childCount > 1)
        {
            prevWaypoint = _waypointRoot.GetChild(_waypointRoot.childCount - 2).GetComponent<Waypoint>();

            while(prevWaypoint.NextPoint)
            {
                prevWaypoint = prevWaypoint.NextPoint;
            }

            waypoint.transform.position = prevWaypoint.transform.forward * _creatingStep + prevWaypoint.transform.position;
            waypoint.transform.rotation = prevWaypoint.transform.rotation;

            prevWaypoint.NextPoint = waypoint;
            waypoint.PrevPoint = prevWaypoint;
        }
        else
        {
            waypoint.transform.localPosition = Vector3.zero;
        }

        Selection.activeObject = waypointObject;
    }

    public void CreatePreviousWaypoint(Waypoint nextWaypoint)
    {
        GameObject waypointObject = new GameObject("Waypoint " + _waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(_waypointRoot, false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();

        if (nextWaypoint != null)
        {
            if (nextWaypoint.PrevPoint)
            {
                waypoint.transform.position = Vector3.Lerp(nextWaypoint.transform.position, nextWaypoint.PrevPoint.transform.position, 0.5f);
                waypoint.transform.rotation = Quaternion.Lerp(nextWaypoint.transform.rotation, nextWaypoint.PrevPoint.transform.rotation, 0.5f);

                waypoint.PrevPoint = nextWaypoint.PrevPoint;
                waypoint.PrevPoint.NextPoint = waypoint;

                waypoint.NextPoint = nextWaypoint;
                waypoint.NextPoint.PrevPoint = waypoint;
            }
            else
            {
                waypoint.transform.position = -nextWaypoint.transform.forward * _creatingStep + nextWaypoint.transform.position;
                waypoint.transform.rotation = nextWaypoint.transform.rotation;

                nextWaypoint.PrevPoint = waypoint;
                waypoint.NextPoint = nextWaypoint;
            }
        }
        else if (_waypointRoot.childCount > 1)
        {
            nextWaypoint = _waypointRoot.GetChild(0).GetComponent<Waypoint>();

            while (nextWaypoint.PrevPoint)
            {
                nextWaypoint = nextWaypoint.PrevPoint;
            }

            waypoint.transform.position = -nextWaypoint.transform.forward * _creatingStep + nextWaypoint.transform.position;
            waypoint.transform.rotation = nextWaypoint.transform.rotation;

            nextWaypoint.PrevPoint = waypoint;
            waypoint.NextPoint = nextWaypoint;
        }
        else
        {
            waypoint.transform.localPosition = Vector3.zero;
        }

        Selection.activeObject = waypointObject;
    }

    public void CreateNewBranch()
    {

    }
}
