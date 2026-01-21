using UnityEngine;

public class UnitHUD : MonoBehaviour
{
    [SerializeField] private UnitSelectorHUD _unitSelector;
    [SerializeField] private UnitTopHUD _topHUD;

    [HideInInspector] public Unit UnitController;

    public void Initialize()
    {
        _unitSelector.SetSelector(false, UnitController.Stats.Size);
        _topHUD.Close(UnitController.Stats.Size);
        _topHUD.UpdateHealthBar(UnitController.Stats.Health, UnitController.Stats.MaxHealth);
    }

    public void OnSelectionStart()
    {
        _unitSelector.SetSelector(true, UnitController.Stats.Size);
        _topHUD.Open(UnitController.Stats.Size);
    }

    public void OnSelectionEnd()
    {
        _unitSelector.SetSelector(false, UnitController.Stats.Size);
        _topHUD.Close(UnitController.Stats.Size);
    }
}
