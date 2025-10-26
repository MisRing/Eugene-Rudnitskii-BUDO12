using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyUnit : MonoBehaviour, IDamageable
{
    private EnemyAnimator _animator;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private int _strongDamage = 10;

    private void Awake()
    {
        _animator = GetComponent<EnemyAnimator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void GetHit(DamageData damageData, Vector2 point)
    {
        _spriteRenderer.flipX = damageData.Direction.x < 0;

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
