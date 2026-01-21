using UnityEngine;

[RequireComponent(typeof(UnitStats))]
[RequireComponent(typeof(UnitCommandController), typeof(UnitAnimator))]
[RequireComponent (typeof(UnitMovement), typeof(UnitCombat))]
public class Unit : MonoBehaviour
{
    [HideInInspector] public UnitStats Stats;
    [HideInInspector] public UnitCommandController CommandController;
    [HideInInspector] public UnitAnimator AnimatorController;
    [HideInInspector] public UnitMovement Movement;
    [HideInInspector] public UnitCombat Combat;

    [SerializeField] private string _name;
    [SerializeField] private bool _selected = false;
    [HideInInspector] public string Name { get { return _name; }}

    private void Awake()
    {
        Stats = GetComponent<UnitStats>();
        CommandController = GetComponent<UnitCommandController>();
        AnimatorController = GetComponent<UnitAnimator>();
        Movement = GetComponent<UnitMovement>();
        Combat = GetComponent<UnitCombat>();

        Stats.UnitController = this;
        CommandController.UnitController = this;
        AnimatorController.UnitController = this;
        Movement.UnitController = this;
        Combat.UnitController = this;
    }

    public Unit Sellect()
    {
        _selected = true;
        return this;
    }

    public void Unsellect()
    {
        _selected = false;
    }
}
