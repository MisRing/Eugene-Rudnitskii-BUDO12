using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnimator : MonoBehaviour
{
    [SerializeField] private Transform _engine;
    [SerializeField] private float _baseScale = 1.4f;
    [SerializeField] private float _scaleMultiplier = 0.1f;
    
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!_engine) return;

        float scale = _baseScale;
        if (_rb.rotation.eulerAngles.y == 0)
        {
            scale += _rb.velocity.z * _scaleMultiplier;
        }
        else
        {
            scale -= _rb.velocity.z * _scaleMultiplier;
        }
        
        _engine.localScale = new Vector3(scale, scale, scale);
    }
}
