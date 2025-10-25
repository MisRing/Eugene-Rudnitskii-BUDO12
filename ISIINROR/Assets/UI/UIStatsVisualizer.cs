using UnityEngine.UI;
using UnityEngine;

public class UIStatsVisualizer : MonoBehaviour
{
    [SerializeField] private PlayerService _playerService;

    [Header("Names")]
    [SerializeField] private Text _characterName;

    [Header("Stat text")]
    [SerializeField] private Text _damageText;
    [SerializeField] private Text _attackSpeedText;
    [SerializeField] private Text _movementSpeedText;
    [SerializeField] private Text _jumpsText;
    [SerializeField] private Text _maxHealthText;
    [SerializeField] private Text _armorText;

    [Header("Bars")]
    [SerializeField] private UIBarComponent _hpBar;

    public void Start()
    {
        ForceStatUpdate();
        _characterName.text = _playerService.Stats.Name;
    }

    private void OnEnable()
    {
        if (!_playerService) return;
        _playerService.StatsVisualizer = this;
        if (!_playerService.Stats) return;


        SubscribeStats();
    }

    private void OnDisable()
    {
        if (!_playerService) return;
        if (!_playerService.Stats) return;

        UnsubscriveStats();
    }

    public void SubscribeStats()
    {
        _playerService.Stats.Damage.OnStatChanged += StatDMUpdate;
        _playerService.Stats.AttackSpeed.OnStatChanged += StatASUpdate;
        _playerService.Stats.MoveSpeed.OnStatChanged += StatMSUpdate;
        _playerService.Stats.Jumps.OnStatChanged += StatJMUpdate;
        _playerService.Stats.MaxHealth.OnStatChanged += StatHPUpdate;
        _playerService.Stats.Armor.OnStatChanged += StatARUpdate;

        _playerService.Stats.OnHPChanged += BarHPUpdate;
    }

    private void UnsubscriveStats()
    {
        _playerService.Stats.Damage.OnStatChanged -= StatDMUpdate;
        _playerService.Stats.AttackSpeed.OnStatChanged -= StatASUpdate;
        _playerService.Stats.MoveSpeed.OnStatChanged -= StatMSUpdate;
        _playerService.Stats.Jumps.OnStatChanged -= StatJMUpdate;
        _playerService.Stats.MaxHealth.OnStatChanged -= StatHPUpdate;
        _playerService.Stats.Armor.OnStatChanged -= StatARUpdate;

        _playerService.Stats.OnHPChanged -= BarHPUpdate;
    }

    public void ForceStatUpdate()
    {
        StatDMUpdate(_playerService.Stats.Damage.Value);
        StatASUpdate(_playerService.Stats.AttackSpeed.Value);
        StatMSUpdate(_playerService.Stats.MoveSpeed.Value);
        StatJMUpdate(_playerService.Stats.Jumps.Value);
        StatHPUpdate(_playerService.Stats.MaxHealth.Value);
        StatARUpdate(_playerService.Stats.Armor.Value);
    }

    private void BarHPUpdate(int currentValue, int maxValue)
    {
        _hpBar.ChangeBar(currentValue, maxValue);
    }

    private void StatDMUpdate(float value)
    {
        _damageText.text = Mathf.RoundToInt(value).ToString();
    }

    private void StatASUpdate(float value)
    {
        _attackSpeedText.text = (Mathf.RoundToInt(value * 100) / 100f).ToString();
    }

    private void StatMSUpdate(float value)
    {
        _movementSpeedText.text = Mathf.RoundToInt(value).ToString();
    }

    private void StatJMUpdate(float value)
    {
        _jumpsText.text = Mathf.RoundToInt(value).ToString();
    }

    private void StatHPUpdate(float value)
    {
        _maxHealthText.text = Mathf.RoundToInt(value).ToString();

        BarHPUpdate(_playerService.Stats.CurrentHealth, Mathf.FloorToInt(value));
    }

    private void StatARUpdate(float value)
    {
        _armorText.text = Mathf.RoundToInt(value).ToString();
    }
}
