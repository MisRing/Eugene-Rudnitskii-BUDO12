using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyFlyComponent))]
//[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(ShipAnimator))]
public class EnemyShipService : MonoBehaviour, IEnemy
{
    [HideInInspector]
    public EnemyFlyComponent FlyComponent;
    //[HideInInspector]
    //public PlayerCombat CombatComponent;
    [HideInInspector]
    public ShipAnimator Animator;

    [Header("Enemy settings")]
    public MovementType MovementType;
    public bool IsControllable = true;

    private void Awake()
    {
        FlyComponent = GetComponent<EnemyFlyComponent>();
        //CombatComponent = GetComponent<PlayerCombat>();
        Animator = GetComponent<ShipAnimator>();
    }

    public void Initialize(EnemyData data)
    {

    }
}