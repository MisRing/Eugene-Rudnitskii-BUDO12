using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour, IReturnable
{
    [SerializeField] private float _lifeTime = 1f;
    [SerializeField] public PooledObjectType Type { get; set; }
    public event Action<GameObject, PooledObjectType> Return;

    private void OnEnable()
    {
        Invoke(nameof(Return), _lifeTime);
    }

    public void ReturnThis()
    {
        Return?.Invoke(gameObject, Type);
    }
}
