using UnityEngine;

public class UnitStats : MonoBehaviour
{
    [HideInInspector] public Unit UnitController;

    [Header("Main stats")]
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _viewRange = 15f;

    [Header("Combat")]
    [SerializeField] private AttackType _attackType = AttackType.Melee;
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private float _attackSpeed = 0.75f;
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _agrRange = 10f;

    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 300f;
    [SerializeField] private float _rotationSpeed = 7.5f;
    [SerializeField] private float _size = 1f;
    
    // Main Stats
    public float MaxHealth { get { return _maxHealth; } }
    public float Health { get { return _health; } }
    public float ViewRange { get { return _viewRange; } }

    // Combat stats
    public AttackType AttackType { get { return _attackType; } }
    public float AttackRange { get { return _attackRange; } }
    public float AttackSpeed { get { return _attackSpeed; } }
    public float Damage { get { return _damage; } }
    public float AgrRange { get { return _agrRange; } }

    // Movement Stats
    public float MoveSpeed { get { return _moveSpeed; } }
    public float RotationSpeed { get { return _rotationSpeed; } }
    public float Size { get { return _size; } }
}


public enum AttackType
{
    Melee = 0,
    Range = 1
}
