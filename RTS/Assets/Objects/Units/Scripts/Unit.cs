using UnityEngine;

[RequireComponent (typeof(UnitStats), typeof(UnitCommandController))]
[RequireComponent (typeof(UnitMovement), typeof(UnitCombat))]
public class Unit : MonoBehaviour
{
    [HideInInspector] public UnitStats Stats;
    [HideInInspector] public UnitCommandController CommandController;
    [HideInInspector] public UnitMovement Movement;
    [HideInInspector] public UnitCombat Combat;

    [SerializeField] private string _name;
    [HideInInspector] public string Name { get { return _name; }}

    private void Awake()
    {
        Stats = GetComponent<UnitStats>();
        CommandController = GetComponent<UnitCommandController>();
        Movement = GetComponent<UnitMovement>();
        Combat = GetComponent<UnitCombat>();

        Stats.UnitController = this;
        CommandController.UnitController = this;
        Movement.UnitController = this;
        Combat.UnitController = this;
    }
}
