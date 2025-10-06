using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _followSpeed = 10f;

    private void LateUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, _target.position, _followSpeed * Time.deltaTime);
    }
}
