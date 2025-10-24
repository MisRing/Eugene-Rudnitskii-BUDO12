using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    [SerializeField]
    private Transform _bottomSpawnPoint;
    [SerializeField]
    private GameObject _jumpEffectPref;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMove(bool move)
    {
        _animator.SetBool("Move", move);
    }

    public void SetGround(bool onGround)
    {
        _animator.SetBool("OnGround", onGround);
    }

    public void SetHit()
    {
        _animator.SetTrigger("Hit");
    }

    public void SetJump()
    {
        _animator.SetTrigger("Jump");
    }

    public void SetDoubleJump()
    {
        Instantiate(_jumpEffectPref, _bottomSpawnPoint.transform.position, Quaternion.identity);

        _animator.SetTrigger("DoubleJump");
    }

    public void SetVerticalVelocity(float yVelocity)
    {
        _animator.SetFloat("VerticalVelocity", yVelocity);
    }
}
