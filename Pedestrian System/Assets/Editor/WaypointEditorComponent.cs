using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class WaypointEditorComponent : EditorWindow
{
    private Transform _waypointRoot;
    private float _defaultRadius = 0.5f;
    [Range(0f, 1f)] private float _defaultPriority = 0.5f;
    private float _creatingStep = 1f;
    private bool _bilateralWays = true;

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

        if (_waypointRoot == null)
        {
            EditorGUILayout.HelpBox("Choose Waypoint Root!", MessageType.Warning);
        }
        else
        {
            _defaultRadius = EditorGUILayout.FloatField("Radius", _defaultRadius);
            _defaultPriority = EditorGUILayout.FloatField("Priority", _defaultPriority);
            _creatingStep = EditorGUILayout.FloatField("Step", _creatingStep);

            GUILayout.Space(5f);

            _bilateralWays = EditorGUILayout.Toggle("Bilateral Ways", _bilateralWays);

            GUILayout.Space(10f);

            DrawButtons();
        }

        GUILayout.Space(10f);

        WaypointDebugDraw.IsDebugGizmo = EditorGUILayout.Toggle("Show Waypoints Debug", WaypointDebugDraw.IsDebugGizmo);
    }

    public void DrawButtons()
    {
        GUILayout.Label("Creating Waypoints");

        if (GUILayout.Button("Create Waypoint"))
        {
            CreateSoloWaypoint();
        }

        if (GUILayout.Button("Create Connected Waypoint"))
        {
            CreateConnectedWaypoint();
        }

        if (GUILayout.Button("Create Intermediate Waypoint"))
        {
            CreateIntermediateWaypoint();
        }
        GUILayout.Space(10f);

        GUILayout.Label("Creating Ways");

        if (GUILayout.Button("Create Way"))
        {
            CreateWay();
        }
        GUILayout.Space(10f);

        GUILayout.Label("Deletion Waypoints");

        if (GUILayout.Button("Delete Current Waypoint"))
        {
            DeleteWaypoint();
        }
        GUILayout.Space(10f);
    }

    public Waypoint CreateWaypoint()
    {
        GameObject waypointObject = new GameObject("Waypoint " + _waypointRoot.childCount, new System.Type[] { typeof(Waypoint), typeof(WaypointConnection) });
        waypointObject.transform.SetParent(_waypointRoot, false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
        waypoint.ConnectionComponent = waypoint.GetComponent<WaypointConnection>();
        waypoint.Radius = _defaultRadius;
        waypoint.ConnectionComponent.Priority = _defaultPriority;

        return waypoint;
    }

    public void CreateSoloWaypoint()
    {
        Waypoint waypoint = CreateWaypoint();

        waypoint.transform.localPosition = Vector3.zero;

        SortWaypoints();
        Selection.activeGameObject = waypoint.gameObject;
    }

    public void CreateConnectedWaypoint()
    {
        Waypoint selectedWaypoint = null;
        if (Selection.activeGameObject)
        {
            selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();
        }

        if (!selectedWaypoint && _waypointRoot.childCount <= 1)
        {
            CreateSoloWaypoint();
            return;
        }

        Waypoint waypoint = CreateWaypoint();

        if (!selectedWaypoint && _waypointRoot.childCount > 1)
        {
            selectedWaypoint = _waypointRoot.GetChild(_waypointRoot.childCount - 2).GetComponent<Waypoint>();
        }

        if (selectedWaypoint.ConnectionComponent.ConnectedWaypoints == null)
        {
            selectedWaypoint.ConnectionComponent.ConnectedWaypoints = new List<Waypoint>();
        }

        waypoint.transform.eulerAngles = selectedWaypoint.transform.eulerAngles
                                        + new Vector3(0f, 40f * selectedWaypoint.ConnectionComponent.ConnectedWaypoints.Count, 0f);
        waypoint.transform.position = waypoint.transform.forward * _creatingStep + selectedWaypoint.transform.position;

        if (_bilateralWays)
        {
            waypoint.ConnectionComponent.ConnectedWaypoints.Add(selectedWaypoint);
        }
        selectedWaypoint.ConnectionComponent.ConnectedWaypoints.Add(waypoint);

        SortWaypoints();
        Selection.activeGameObject = waypoint.gameObject;
    }

    public void CreateIntermediateWaypoint()
    {
        if (Selection.gameObjects.Length != 2
            || !Selection.gameObjects[0].GetComponent<Waypoint>()
            || !Selection.gameObjects[1].GetComponent<Waypoint>())
        {
            Debug.LogWarning("Select two waypoints to create way!");
            return;
        }

        Waypoint fromWP = Selection.gameObjects[0].GetComponent<Waypoint>();
        Waypoint toWP = Selection.gameObjects[1].GetComponent<Waypoint>();

        fromWP.ConnectionComponent.ConnectedWaypoints.Remove(toWP);
        toWP.ConnectionComponent.ConnectedWaypoints.Remove(fromWP);

        Waypoint waypoint = CreateWaypoint();

        waypoint.transform.position = (fromWP.transform.position + toWP.transform.position) / 2f;
        waypoint.transform.LookAt(toWP.transform);

        fromWP.ConnectionComponent.ConnectedWaypoints.Add(waypoint);
        waypoint.ConnectionComponent.ConnectedWaypoints.Add(toWP);
        if (_bilateralWays)
        {
            waypoint.ConnectionComponent.ConnectedWaypoints.Add(fromWP);
            toWP.ConnectionComponent.ConnectedWaypoints.Add(waypoint);
        }

        SortWaypoints();
        Selection.activeGameObject = waypoint.gameObject;
    }

    public void DeleteWaypoint()
    {
        Waypoint waypoint = null;
        if (Selection.activeGameObject)
        {
            waypoint = Selection.activeGameObject.GetComponent<Waypoint>();
        }
        if (waypoint)
        {
            DestroyImmediate(waypoint.gameObject);
            SortWaypoints();
            return;
        }

        Debug.LogWarning("The selected object is missing or does not have a `Waypoint` component.");
    }

    public void CreateWay()
    {
        if(Selection.gameObjects.Length != 2
            || !Selection.gameObjects[0].GetComponent<Waypoint>()
            || !Selection.gameObjects[1].GetComponent<Waypoint>())
        {
            Debug.LogWarning("Select two waypoints to create way!");
            return;
        }

        Waypoint fromWP = Selection.gameObjects[0].GetComponent<Waypoint>();
        Waypoint toWP = Selection.gameObjects[1].GetComponent<Waypoint>();

        fromWP.ConnectionComponent.ConnectedWaypoints.Remove(toWP);
        toWP.ConnectionComponent.ConnectedWaypoints.Remove(fromWP);

        if (_bilateralWays)
        {
            toWP.ConnectionComponent.ConnectedWaypoints.Add(fromWP);
        }
        fromWP.ConnectionComponent.ConnectedWaypoints.Add(toWP);
    }

    public void SortWaypoints()
    {
        int n = 0;
        for (int i = 0; i < _waypointRoot.childCount; i++)
        {
            if (_waypointRoot.GetChild(i).GetComponent<Waypoint>() != null)
            {
                _waypointRoot.GetChild(i).gameObject.name = "Waypoint " + n;
                n++;
            }
        }
    }
}
