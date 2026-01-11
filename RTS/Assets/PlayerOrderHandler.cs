using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOrderHandler : MonoBehaviour
{
    public UnitMovement unit;
    private RTS_InputActions _inputControls;

    private void Awake()
    {
        _inputControls = new RTS_InputActions();
    }

    private void OnEnable()
    {
        _inputControls.Enable();
        _inputControls.PlayerControls.MouseClick.performed += MoveOrder;
    }

    private void OnDisable()
    {
        _inputControls.Disable();
        _inputControls.PlayerControls.MouseClick.performed -= MoveOrder;
    }

    private void MoveOrder(InputAction.CallbackContext context)
    {
        bool addToOrderQ = _inputControls.PlayerControls.LeftShift.IsPressed();
        unit.AddOrder(new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f)), addToOrderQ);
    }
}
