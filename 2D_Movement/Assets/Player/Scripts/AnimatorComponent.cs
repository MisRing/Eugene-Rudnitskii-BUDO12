using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorComponent : MonoBehaviour
{
    [SerializeField] private float _minFallSpeed = 5f;
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

    public void StartJump()
    {
        _animator.SetTrigger("Jump");
    }

    public void SetGroundState(bool _isGrounded)
    {
        _animator.SetBool("Grounded", _isGrounded);
    }
}
