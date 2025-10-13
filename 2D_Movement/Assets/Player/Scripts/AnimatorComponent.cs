using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorComponent : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetWalkState(float movement)
    {
        movement = Mathf.Abs(movement);
        _animator.SetBool("Walk", movement > 0);
    }

    public void SetVerticalVelocity(float velocityY)
    {
        _animator.SetFloat("VerticalVelocity", velocityY);
    }
}
