using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private bool _moveX, _moveY;
    [SerializeField] private float _moveForce = 0.01f;
    private Transform _cameraTransform;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        if (!_moveX && !_moveY)
            return;

        Vector2 position = _cameraTransform.position * _moveForce * (-1);

        if (!_moveX)
        {
            position.x = 0;
        }
        if (!_moveY)
        {
            position.y = 0;
        }

        transform.position = position;
    }
}
