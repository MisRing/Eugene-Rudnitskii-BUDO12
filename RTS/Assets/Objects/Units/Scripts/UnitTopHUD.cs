using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class UnitTopHUD : MonoBehaviour
{
    [SerializeField] private RectTransform _healthBar;
    [SerializeField] private RectTransform _healthBarTougle;
    [SerializeField] private float _unitSizeMod = 1.5f;
    [SerializeField] private float _selectedSizeMod = 1.2f;
    [SerializeField] private float _selectedYSize = 1f;


    public void Open(float unitSize)
    {
        _healthBar.localScale = new Vector2(unitSize * _unitSizeMod * _selectedSizeMod, _selectedYSize);
    }

    public void Close(float unitSize)
    {
        _healthBar.localScale = new Vector2(unitSize * _unitSizeMod, 1f);
    }

    public void UpdateHealthBar(float health, float maxHealth)
    {
        float percent = health / maxHealth;
        _healthBarTougle.localScale = new Vector2(percent, 1);
    }

    private void Update()
    {
        Vector3 targetRotation = Camera.main.transform.eulerAngles;
        transform.eulerAngles = targetRotation;
    }
}
