using UnityEngine;

[RequireComponent(typeof(UnitStats))]
[RequireComponent(typeof(UnitCommandController))]
[RequireComponent(typeof(UnitAnimator))]
[RequireComponent(typeof(UnitHUD))]
[RequireComponent(typeof(UnitMovement))]
[RequireComponent(typeof(UnitCombat))]
public class Unit : MonoBehaviour
{
    [HideInInspector] public UnitStats Stats;
    [HideInInspector] public UnitCommandController CommandController;
    [HideInInspector] public UnitAnimator AnimatorController;
    [HideInInspector] public UnitHUD HUDController;
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
        HUDController = GetComponent<UnitHUD>();
        Movement = GetComponent<UnitMovement>();
        Combat = GetComponent<UnitCombat>();

        Stats.UnitController = this;
        CommandController.UnitController = this;
        AnimatorController.UnitController = this;
        HUDController.UnitController = this;
        Movement.UnitController = this;
        Combat.UnitController = this;

        HUDController.Initialize();
    }

    public Unit Sellect()
    {
        _selected = true;
        HUDController.OnSelectionStart();
        return this;
    }

    public void Unsellect()
    {
        _selected = false;
        HUDController.OnSelectionEnd();
    }
}
