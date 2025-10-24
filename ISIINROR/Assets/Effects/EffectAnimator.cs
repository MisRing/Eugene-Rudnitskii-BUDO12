using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAnimator : MonoBehaviour
{
    public void OnAnimationEnds()
    {
        Destroy(gameObject);
    }
}
