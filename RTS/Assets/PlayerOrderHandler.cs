using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOrderHandler : MonoBehaviour
{
    public Unit unit;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject cursorPref;
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
        Ray ray = Camera.main.ScreenPointToRay(_inputControls.CameraControls.MousePosition.ReadValue<Vector2>());
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100f, groundLayer))
        {
            Vector3 point = hit.point;
            bool addToOrderQ = _inputControls.PlayerControls.LeftShift.IsPressed();
            unit.Movement.AddOrder(point, addToOrderQ);

            GameObject.Instantiate(cursorPref, point, Quaternion.identity);
        }
    }
}
