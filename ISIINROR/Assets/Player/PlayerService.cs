using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats)), RequireComponent(typeof(MovementComponent)), RequireComponent(typeof(PlayerAnimator))]
public class PlayerService : MonoBehaviour
{
    [HideInInspector] public PlayerStats Stats;
    [HideInInspector] public MovementComponent MovementComponent;
    [HideInInspector] public PlayerAnimator Animator;
    [HideInInspector] public BulletPool BulletPool;

    [HideInInspector] public UIStatsVisualizer StatsVisualizer;

    public event Action OnHightFall;

    private void Awake()
    {
        Stats = GetComponent<PlayerStats>();
        MovementComponent = GetComponent<MovementComponent>();
        Animator = GetComponent<PlayerAnimator>();
        BulletPool = GetComponent<BulletPool>();

        StatsVisualizer.SubscribeStats();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && Stats.CanAttack())
        {
            Attack();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetHit(7f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

        }
    }

    private void Attack()
    {
        BulletPool.GetBullet(Stats.Damage.Value, 30f, false, 3f);
    }

    public void GetHit(float damage)
    {
        Stats.TakeDamage(damage);
        Animator.SetHit();
        MovementComponent.InterruptVelocity(0.1f);
    }

    public void InvokeOnHightFall()
    {
        OnHightFall?.Invoke();
    }
}
