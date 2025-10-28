using System;
using UnityEngine;

namespace Characters.Player
{
    [RequireComponent(typeof(PlayerStats))]
    [RequireComponent(typeof(PlayerConditions))]
    [RequireComponent(typeof(PlayerAnimator))]
    [RequireComponent(typeof(MovementComponent))]
    [RequireComponent(typeof(PlayerCombat))]
    public class PlayerService : MonoBehaviour
    {
        [HideInInspector] public PlayerStats Stats;
        [HideInInspector] public PlayerConditions Conditions;
        [HideInInspector] public MovementComponent MovementComponent;
        [HideInInspector] public PlayerAnimator Animator;
        [HideInInspector] public PlayerCombat PlayerCombat;

        [HideInInspector] public UIStatsVisualizer StatsVisualizer;

        private void Awake()
        {
            Stats = GetComponent<PlayerStats>();
            Conditions = GetComponent<PlayerConditions>();
            MovementComponent = GetComponent<MovementComponent>();
            Animator = GetComponent<PlayerAnimator>();
            PlayerCombat = GetComponent<PlayerCombat>();

            StatsVisualizer?.SubscribeStats();
        }
    }
}
