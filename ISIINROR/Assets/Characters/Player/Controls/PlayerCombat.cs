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

        // for debug {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DamageData damageData = new DamageData(
                Random.Range(3, 20),
                this,
                LayerMask.NameToLayer("Player"),
                Vector2.zero
                );
            GetHit(damageData);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _playerService.Stats.Heal(Random.Range(3, 20));
        }
        // }....
    }

    public void Attack()
    {
        bool isLookRight = _playerService.MovementComponent.IsLookRight;

        bool isCritical = Random.Range(0f, 100f) <= _playerService.Stats.CriticalChance.Value;

        DamageData damageData = new DamageData(
            Mathf.FloorToInt(_playerService.Stats.Damage.Value),
            this,
            _targets,
            isLookRight ? Vector2.right : Vector2.left,
            isCritical
            );

        Vector2 realSpawnPoint = transform.position;
        realSpawnPoint += isLookRight ? _bulletSpawnPoint : new Vector2(-_bulletSpawnPoint.x, _bulletSpawnPoint.y);

        _bulletPool.GetBullet(damageData, 40f, isLookRight, realSpawnPoint);
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

    public void GetHitData(HitData hitData)
    {
        
    }

    public void GetHit(DamageData damageData)
    {
        int realDamage = GetRealDamage(damageData, GetArmorResist(_playerService.Stats.Armor.Value, damageData.ArmorBreak));

        _playerService.Stats.TakeDamage(realDamage);
        _playerService.Animator.SetHit();
        _playerService.MovementComponent.InterruptVelocity(0.1f);
    }

    private int GetRealDamage(DamageData damageData, float damageMultiplier)
    {
        int realDamage = Mathf.FloorToInt(damageData.Damage * damageMultiplier);

        if (realDamage < 1)
        {
            realDamage = 1;
        }

        return realDamage;
    }

    private float GetArmorResist(float armor, float armorBrake)
    {
        return 100f / (100f + armor - armorBrake);
    }
}
