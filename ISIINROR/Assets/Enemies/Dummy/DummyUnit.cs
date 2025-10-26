using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyUnit : MonoBehaviour, IDamageable
{
    private EnemyAnimator _animator;

    [SerializeField] private int _strongDamage = 10;

    private void Awake()
    {
        _animator = GetComponent<EnemyAnimator>();
    }

    public void GetHit(DamageData damageData)
    {
        if(damageData.IsCritical)
        {
            _animator.SetCritHit();
        }
        else if(damageData.Damage >= _strongDamage)
        {
            _animator.SetStrongHit();
        }
        else
        {
            _animator.SetWeakHit();
        }
    }
}
