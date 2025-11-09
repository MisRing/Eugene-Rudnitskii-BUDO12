using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerFlyComponent : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _flySpeed = 1f;
    [SerializeField] private float _tilt = 4f;
    [SerializeField] private Boundary _boundary;

    [Header("Dash Settings")]
    [SerializeField] private float _dashForce = 15f;
    [SerializeField] private float _dashReduction = 30f;
    [SerializeField] private float _dashDuration = 0.2f;
    [SerializeField] private float _dashCooldown = 1f;

    private Rigidbody _rb;
    private Vector3 _movement;

    private bool _isDashing = false;
    private bool _canDash = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        if (_isDashing) return;

        _movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            StartCoroutine(DashRoutine());
        }
    }

    private void FixedUpdate()
    {
        if (!_isDashing)
        {
            _rb.velocity = _movement * _flySpeed;
            _rb.rotation = Quaternion.Euler(0f, 0f, _rb.velocity.x * -_tilt);
        }

        if (_boundary)
        {
            _rb.position = _boundary.LimitObject(_rb.position);
        }
    }

    private IEnumerator DashRoutine()
    {
        _isDashing = true;
        _canDash = false;

        // определяем направление даша (по последнему движению)
        float dashDir = Mathf.Sign(_movement.x);
        if (dashDir == 0) dashDir = 1f; // если стоял на месте — рывок вправо

        Vector3 dashVelocity = new Vector3(dashDir * _dashForce, 0f, 0f);
        _rb.velocity = dashVelocity;

        // вращение по оси Z на 360°
        float elapsed = 0f;
        while (elapsed < _dashDuration)
        {
            float angle = Mathf.Lerp(0, 360, elapsed / _dashDuration);
            //_rb.rotation = Quaternion.Euler(0f, 0f, angle);
            elapsed += Time.deltaTime;

            //dashVelocity = new Vector3(Mathf.Clamp(Mathf.Abs(dashVelocity.x) - Time.deltaTime * _dashReduction, 0f, float.MaxValue) * dashDir, 0f, 0f);

            yield return null;

            dashVelocity = Vector3.Lerp(dashVelocity, Vector3.right * dashDir * _flySpeed, _dashReduction * Time.deltaTime);

            _rb.velocity = dashVelocity;
        }

        // возвращаем поворот
        transform.localRotation = Quaternion.identity;
        _isDashing = false;

        // небольшой кулдаун
        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }
}
