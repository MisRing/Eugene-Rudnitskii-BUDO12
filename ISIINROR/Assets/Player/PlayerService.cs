using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats)), RequireComponent(typeof(MovementComponent)), RequireComponent(typeof(PlayerAnimator))]
public class PlayerService : MonoBehaviour
{
    [HideInInspector]
    public PlayerStats Stats;
    [HideInInspector]
    public MovementComponent MovementComponent;
    [HideInInspector]
    public PlayerAnimator Animator;


    private void Awake()
    {
        Stats = GetComponent<PlayerStats>();
        MovementComponent = GetComponent<MovementComponent>();
        Animator = GetComponent<PlayerAnimator>();
    }
}
