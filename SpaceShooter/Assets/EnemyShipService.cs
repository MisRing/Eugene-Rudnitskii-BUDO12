using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyFlyComponent))]
[RequireComponent(typeof(EnemyCombat))]
[RequireComponent(typeof(ShipAnimator))]
public class EnemyShipService : MonoBehaviour, IEnemy
{
    [HideInInspector]
    public EnemyFlyComponent FlyComponent;
    [HideInInspector]
    public EnemyCombat CombatComponent;
    [HideInInspector]
    public ShipAnimator Animator;

    [Header("Enemy settings")]
    public MovementType MovementType;
    public FireType FireType;
    public bool IsControllable = false;

    private void Awake()
    {
        FlyComponent = GetComponent<EnemyFlyComponent>();
        CombatComponent = GetComponent<EnemyCombat>();
        Animator = GetComponent<ShipAnimator>();
    }

    public void Initialize(EnemyData data)
    {

    }
}