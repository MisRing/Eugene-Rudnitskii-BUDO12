using UnityEngine;

public class UnitHUD : MonoBehaviour
{
    [SerializeField] private UnitSelectorHUD _unitSelector;

    [HideInInspector] public Unit UnitController;

    public void Initialize()
    {
        _unitSelector.SetSelector(false, UnitController.Stats.Size);
    }

    public void OnSelectionStart()
    {
        _unitSelector.SetSelector(true, UnitController.Stats.Size);
    }

    public void OnSelectionEnd()
    {
        _unitSelector.SetSelector(false, UnitController.Stats.Size);
    }
}
