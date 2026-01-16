using UnityEngine;

public class UnitStats : MonoBehaviour
{
    [HideInInspector] public Unit UnitController;

    [Header("Stats")]
    public float MoveSpeed = 15f;
    public float RotationSpeed = 20f;
}
