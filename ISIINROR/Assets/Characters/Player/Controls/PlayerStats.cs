using System;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerStats : MonoBehaviour
    {
        [Header("Player character")]
        [SerializeField] private string _name = "nameless";
        [SerializeField] private string _characterTitle = "One nameless hero";

        [Header("Player stats")]
        public StatProperty Damage = new StatProperty(12, false, true, true);
        public StatProperty CriticalChance = new StatProperty(1, false, true, true);
        public StatProperty AttackSpeed = new StatProperty(0.5f, true);
        public StatProperty MoveSpeed = new StatProperty(30);
        public StatProperty MaxHealth = new StatProperty(200, false, true, true);
        public StatProperty Armor = new StatProperty(10, false, false, true);
        public StatProperty Jumps = new StatProperty(1, false, true, true);

        [Header("Current stats")]
        [SerializeField] private float _attackCooldown = 0;
        [SerializeField] private int _currentHealth = 0;

        public string Name { get { return _name; } }
        public string CharacterTitle { get { return _characterTitle; } }
        public int CurrentHealth { get { return _currentHealth; } }

        //Events
        public event Action<int, int> OnHealthChanged;


        private void Awake()
        {
            Damage.UpdateStat();
            CriticalChance.UpdateStat();
            AttackSpeed.UpdateStat();
            MoveSpeed.UpdateStat();
            MaxHealth.UpdateStat();
            Armor.UpdateStat();
            Jumps.UpdateStat();

            _currentHealth = Mathf.FloorToInt(MaxHealth.Value);
        }

        private void Update()
        {
            _attackCooldown -= Time.deltaTime;
        }

        public bool CheckAttackCooldown(bool tryAttack = true)
        {
            if (_attackCooldown <= 0)
            {
                if (tryAttack)
                {
                    _attackCooldown = 1f / AttackSpeed.Value;
                }

                return true;
            }
            else return false;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            _currentHealth = Mathf.Clamp(_currentHealth, 0, Mathf.FloorToInt(MaxHealth.Value));

            OnHealthChanged?.Invoke(_currentHealth, Mathf.FloorToInt(MaxHealth.Value));
        }

        public void Heal(int heal)
        {
            _currentHealth += heal;

            _currentHealth = Mathf.Clamp(_currentHealth, 0, Mathf.FloorToInt(MaxHealth.Value));

            OnHealthChanged?.Invoke(_currentHealth, Mathf.FloorToInt(MaxHealth.Value));
        }
    }
}