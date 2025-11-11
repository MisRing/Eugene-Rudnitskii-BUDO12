using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerFlyComponent : ShipFlyComponent
{
    private void Update()
    {
        if (_isDashing) return;

        _movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash && _movement.x != 0)
        {
            StartCoroutine(DashRoutine());
        }
    }
}
