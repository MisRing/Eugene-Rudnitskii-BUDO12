using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float _cameraSpeed = 10f;
    [SerializeField, Range(0f, 0.05f)] private float _moveBorderPercent = 0.04f;

    private RTS_InputActions _inputControls;

    private void Awake()
    {
        _inputControls = new RTS_InputActions();
    }

    private void OnEnable()
    {
        _inputControls.Enable();
    }

    private void OnDisable()
    {
        _inputControls.Disable();
    }

    private void Update()
    {
        Vector3 direction = new Vector3(_inputControls.CameraControls.MoveHorizontal.ReadValue<float>(),
                                        0f,
                                        _inputControls.CameraControls.MoveVertical.ReadValue<float>());

        Vector2 mouseDirection = GetMouseDirection();

        direction.x = direction.x == 0 ? mouseDirection.x : direction.x;
        direction.z = direction.z == 0 ? mouseDirection.y : direction.z;

        transform.position += direction * _cameraSpeed * Time.deltaTime;
    }

    private Vector2 GetMouseDirection()
    {
        Vector2 mousePos = _inputControls.CameraControls.MousePosition.ReadValue<Vector2>();
        float screenSizeX = Screen.width;
        float screenSizeY = Screen.height;

        Vector2 mouseDirection = Vector2.zero;

        mouseDirection.x = mousePos.x <= screenSizeX * _moveBorderPercent ? -1 : mousePos.x >= screenSizeX * (1 - _moveBorderPercent) ? 1 : 0;
        mouseDirection.y = mousePos.y <= screenSizeY * _moveBorderPercent ? -1 : mousePos.y >= screenSizeY * (1 - _moveBorderPercent) ? 1 : 0;

        return mouseDirection;
    }
}
