using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerFlyComponent : MonoBehaviour
{
    [SerializeField] private float _flySpeed = 1f;
    [SerializeField] private float _tilt = 4f;
    [SerializeField] private float _dashPower = 5f;
    [SerializeField] private float _dashTime = 0.3f;
    
    [SerializeField] private Boundary _boundary;

    private Rigidbody _rb;

    private Vector3 _movement;
    private bool _isControlled = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        if (_isControlled)
        {
            _movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && _rb.velocity.x != 0)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movement * _flySpeed;

        if (_boundary)
        {
            _rb.position = _boundary.LimitObject(_rb.position);
        }

        _rb.rotation = Quaternion.Euler(0f, 0f, _rb.velocity.x * -_tilt);
    }

    private IEnumerator Dash()
    {
        _isControlled = false;

        _rb.velocity = Vector3.zero;

        _rb.AddForce(Vector3.right * Mathf.Sign(_rb.velocity.x) * _dashPower, ForceMode.Impulse);

        yield return new WaitForSeconds(_dashTime);

        _isControlled = true;
    }
}
