using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerFlyComponent))]
[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(ShipAnimator))]
public class PlayerService : MonoBehaviour
{
    [HideInInspector]
    public PlayerFlyComponent FlyComponent;
    [HideInInspector]
    public PlayerCombat CombatComponent;
    [HideInInspector]
    public ShipAnimator Animator;

    private void Awake()
    {
        FlyComponent = GetComponent<PlayerFlyComponent>();
        CombatComponent = GetComponent<PlayerCombat>();
        Animator = GetComponent<ShipAnimator>();
    }
}
