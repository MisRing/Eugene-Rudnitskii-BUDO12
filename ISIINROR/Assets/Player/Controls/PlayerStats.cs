using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player stats")]
    public StatProperty Damage = new StatProperty(12);
    public StatProperty AttackSpeed = new StatProperty(0.5f, true);
    public StatProperty MoveSpeed = new StatProperty(30);
    public StatProperty MaxHealth = new StatProperty(200);
    public StatProperty Armor = new StatProperty(10);
    public StatProperty Jumps = new StatProperty(1);

    private float _attackCooldown = 0;
    private float _currentHealth = 0;


    private void Awake()
    {
        Damage.UpdateStat();
        AttackSpeed.UpdateStat();
        MoveSpeed.UpdateStat();
        MaxHealth.UpdateStat();
        Jumps.UpdateStat();

        _currentHealth = MaxHealth.Value;
    }

    private void Update()
    {
        _attackCooldown -= Time.deltaTime;
    }

    public bool CanAttack(bool tryAttack = true)
    {
        if(_attackCooldown <= 0)
        {
            if (tryAttack)
            {
                _attackCooldown = 1f / AttackSpeed.Value;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TakeDamage(float damage)
    {
        float realDamage = damage * GetDamageMultiplayer(Armor.Value);

        _currentHealth -= realDamage;

        _currentHealth = Mathf.Clamp(_currentHealth, 0f, MaxHealth.Value);
    }

    private float GetDamageMultiplayer(float armor)
    {
        return 100f / (100f + armor);
    }
}
