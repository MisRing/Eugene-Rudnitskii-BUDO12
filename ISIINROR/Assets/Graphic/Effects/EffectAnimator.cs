using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAnimator : MonoBehaviour
{
    [SerializeField] private bool _return = false;
    public event Action<GameObject> OnAnimationEnds;

    public void EndAnimation()
    {
        if(_return)
        {
            OnAnimationEnds?.Invoke(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
