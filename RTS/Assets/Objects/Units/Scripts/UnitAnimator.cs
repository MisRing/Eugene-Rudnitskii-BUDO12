using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [HideInInspector] public Unit UnitController;

    [SerializeField] private float _animationSpeedDelta = 1.5f;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        Vector3 moveDirection = UnitController.Movement.MovementDirection;

        _animator.SetFloat("DirX", moveDirection.x);
        _animator.SetFloat("DirZ", moveDirection.z);

        _animator.speed = UnitController.Stats.MoveSpeed * _animationSpeedDelta * Time.fixedDeltaTime;
    }
}
