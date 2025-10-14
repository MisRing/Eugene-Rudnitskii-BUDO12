using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeEffect : MonoBehaviour
{
    public void EndAnimation()
    {
        Destroy(gameObject);
    }
}
