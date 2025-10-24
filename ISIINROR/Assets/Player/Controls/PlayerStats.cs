using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player stats")]
    public StatProperty Damage = new StatProperty(12);
    public StatProperty AttackSpeed = new StatProperty(0.5f, true);
    public StatProperty MoveSpeed = new StatProperty(30);
    public StatProperty MaxHealth = new StatProperty(200);
    public StatProperty Jumps = new StatProperty(1);


    private void Awake()
    {
        Damage.UpdateStat();
        AttackSpeed.UpdateStat();
        MoveSpeed.UpdateStat();
        MaxHealth.UpdateStat();
        Jumps.UpdateStat();
    }
}
