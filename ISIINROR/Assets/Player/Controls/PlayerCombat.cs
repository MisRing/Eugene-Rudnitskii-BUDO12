using UnityEngine;

public class PlayerCombat : MonoBehaviour, IDamageable, IDamageDealler
{
    private PlayerService _playerService;

    [SerializeField] private Vector2 _bulletSpawnPoint;
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private LayerMask _targets;

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
        bool isLookRight = _playerService.MovementComponent.IsLookRight;

        bool isCritical = Random.Range(0f, 100f) <= _playerService.Stats.CriticalChance.Value;

        DamageData damageData = new DamageData(
            Mathf.FloorToInt(_playerService.Stats.Damage.Value),
            this as IDamageDealler,
            _targets,
            isLookRight ? Vector2.right : Vector2.left,
            isCritical
            );

        Vector2 realSpawnPoint = transform.position;
        realSpawnPoint += isLookRight ? _bulletSpawnPoint : new Vector2(-_bulletSpawnPoint.x, _bulletSpawnPoint.y);

        _bulletPool.GetBullet(damageData, 40f, isLookRight, realSpawnPoint);
    }

    public void GetHit(int damage)
    {
        _playerService.Stats.TakeDamage(damage);
        _playerService.Animator.SetHit();
        _playerService.MovementComponent.InterruptVelocity(0.1f);
    }

    private void OnDrawGizmosSelected()
    {
        bool isLookRight = true;
        if (_playerService)
        {
            isLookRight = _playerService.MovementComponent.IsLookRight;
        }
        Gizmos.color = Color.yellow;
        Vector2 realSpawnPoint = transform.position;
        realSpawnPoint += isLookRight ? _bulletSpawnPoint : new Vector2(-_bulletSpawnPoint.x, _bulletSpawnPoint.y);
        Gizmos.DrawWireSphere(realSpawnPoint, 0.2f);
    }
}
