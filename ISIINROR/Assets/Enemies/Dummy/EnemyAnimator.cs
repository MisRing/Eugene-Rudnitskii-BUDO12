using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetWeakHit()
    {
        _animator.SetTrigger("Hit0");
    }

    public void SetStrongHit()
    {
        _animator.SetTrigger("Hit1");
    }

    public void SetCritHit()
    {
        _animator.SetTrigger("Hit2");
    }
}
