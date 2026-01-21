using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOrderHandler : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask unitsLayer;
    [SerializeField] private GameObject cursorPref;
    private RTS_InputActions _inputControls;

    private void Awake()
    {
        _inputControls = new RTS_InputActions();
    }

    private void OnEnable()
    {
        _inputControls.Enable();
        _inputControls.PlayerControls.MouseRightClick.performed += MoveOrder;
        _inputControls.PlayerControls.MouseLeftClick.performed += LeftClick;
    }

    private void OnDisable()
    {
        _inputControls.Disable();
        _inputControls.PlayerControls.MouseRightClick.performed -= MoveOrder;
        _inputControls.PlayerControls.MouseLeftClick.performed -= LeftClick;
    }

    private void MoveOrder(InputAction.CallbackContext context)
    {
        if (!_unit) return;

        Ray ray = Camera.main.ScreenPointToRay(_inputControls.CameraControls.MousePosition.ReadValue<Vector2>());
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100f, groundLayer))
        {
            Vector3 point = hit.point;
            bool addToOrderQ = _inputControls.PlayerControls.LeftShift.IsPressed();
            _unit.Movement.AddOrder(point, addToOrderQ);

            GameObject.Instantiate(cursorPref, point, Quaternion.identity);
        }
    }

    private void LeftClick(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(_inputControls.CameraControls.MousePosition.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, unitsLayer))
        {
            _unit = hit.collider.gameObject.GetComponent<Unit>();
            _unit.Sellect();
        }
        else if(_unit)
        {
            _unit.Unsellect();
            _unit = null;
        }
    }
}
