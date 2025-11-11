using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IReturnable
{
    public event Action<GameObject> Return;

    public void ReturnThis();
}
