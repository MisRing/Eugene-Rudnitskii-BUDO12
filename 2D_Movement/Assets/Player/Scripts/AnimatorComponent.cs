using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorComponent : MonoBehaviour
{
    [SerializeField] private float _minFallSpeed = 5f;
    [SerializeField] private Transform _effectTarget;
    [SerializeField] private GameObject _DoubleJumpEffectPref;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetWalkState(float movement)
    {
        _animator.SetBool("Walk", movement != 0);
    }

    public void SetVerticalVelocity(float velocityY)
    {
        _animator.SetFloat("VerticalVelocity", velocityY);
    }

    public void StartJump(bool isDoubleJump = false)
    {
        _animator.SetTrigger("Jump");

        if (isDoubleJump)
        {
            GameObject effect = GameObject.Instantiate(_DoubleJumpEffectPref, _effectTarget.position, Quaternion.identity);
        }
    }

    public void SetGroundState(bool _isGrounded)
    {
        _animator.SetBool("Grounded", _isGrounded);
    }
}
