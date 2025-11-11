using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerFlyComponent))]
[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerService : MonoBehaviour
{
    public PlayerFlyComponent FlyComponent;
    public PlayerCombat CombatComponent;
    public PlayerAnimator Animator;

    private void Awake()
    {
        FlyComponent = GetComponent<PlayerFlyComponent>();
        CombatComponent = GetComponent<PlayerCombat>();
        Animator = GetComponent<PlayerAnimator>();
    }
}
