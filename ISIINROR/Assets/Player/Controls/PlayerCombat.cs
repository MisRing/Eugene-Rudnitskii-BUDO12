using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerService _playerService;

    private void Awake()
    {
        _playerService = GetComponent<PlayerService>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _playerService.Stats.CheckAttackCooldown())
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetHit(Random.Range(3, 20));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _playerService.Stats.Heal(Random.Range(3, 20));
        }
    }

    private void Attack()
    {
        _playerService.BulletPool.GetBullet(_playerService.Stats.Damage.Value, 30f, false, 3f);
    }

    public void GetHit(int damage)
    {
        _playerService.Stats.TakeDamage(damage);
        _playerService.Animator.SetHit();
        _playerService.MovementComponent.InterruptVelocity(0.1f);
    }
}
