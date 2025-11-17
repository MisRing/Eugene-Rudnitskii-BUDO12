using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IReturnable
{
    PooledObjectType Type { get; set; }
    public event Action<GameObject, PooledObjectType> Return;

    public void ReturnThis();
}
