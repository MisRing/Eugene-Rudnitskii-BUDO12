using System;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(BulletPool))]
[RequireComponent(typeof(PlayerCombat))]
public class PlayerService : MonoBehaviour
{
    [HideInInspector] public PlayerStats Stats;
    [HideInInspector] public MovementComponent MovementComponent;
    [HideInInspector] public PlayerAnimator Animator;
    [HideInInspector] public BulletPool BulletPool;
    [HideInInspector] public PlayerCombat PlayerCombat;

    [HideInInspector] public UIStatsVisualizer StatsVisualizer;

    public event Action OnHightFall;

    private void Awake()
    {
        Stats = GetComponent<PlayerStats>();
        MovementComponent = GetComponent<MovementComponent>();
        Animator = GetComponent<PlayerAnimator>();
        BulletPool = GetComponent<BulletPool>();
        PlayerCombat = GetComponent<PlayerCombat>();

        StatsVisualizer.SubscribeStats();
    }

    public void InvokeOnHightFall()
    {
        OnHightFall?.Invoke();
    }
}
