using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPool<T> : MonoBehaviour where T : Component
{
    [Header("Pool Settings")]
    [SerializeField] private T _prefab;
}
