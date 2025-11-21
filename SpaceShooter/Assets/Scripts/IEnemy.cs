using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public interface IEnemy
{
    void Initialize(EnemyData data, ObjectPool objPool);
}

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string Name = "none";
    public EnemyType Type = EnemyType.Asteroid;
    public GameObject Prefab;
    public float Speed = 8f;
    public MovementType MovementType = MovementType.None;
    public FireType FireType = FireType.None;
    public float FireRate = 1f;
}

public enum EnemyType
{
    Asteroid,
    Ship
}

[CustomEditor(typeof(EnemyData))]
public class MyDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EnemyData myData = (EnemyData)target;

        EditorGUILayout.LabelField("Base Properties", EditorStyles.boldLabel);
        myData.Name = EditorGUILayout.TextField("Name", myData.Name);
        myData.Type = (EnemyType)EditorGUILayout.EnumPopup("Type", myData.Type);
        myData.Prefab = (GameObject)EditorGUILayout.ObjectField(
            "Prefab",
            myData.Prefab,
            typeof(GameObject),
            false
        );
        myData.Speed = EditorGUILayout.FloatField("Speed", myData.Speed);

        if (myData.Type == EnemyType.Ship)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Ship Properties", EditorStyles.boldLabel);

            myData.MovementType = (MovementType)EditorGUILayout.EnumPopup("Movement Type", myData.MovementType);
            myData.FireType = (FireType)EditorGUILayout.EnumPopup("Fire Type", myData.FireType);
            myData.FireRate = EditorGUILayout.FloatField("Fire Rate", myData.FireRate);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(myData);
        }
    }
}