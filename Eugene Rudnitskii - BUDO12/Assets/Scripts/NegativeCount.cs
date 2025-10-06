using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeCount : MonoBehaviour
{

    [SerializeField] private int[] array = new int[12] {9, 12, 0, -22, 135, -21, -12, 2356, 222, -1, 2, 99};


    void Start()
    {
        Debug.Log($"Array: {string.Join(", ", array)}");

        Debug.Log($"Negative count: {GetArrayNegative(array).ToString()}");
    }

    private int GetArrayNegative(int[] _array)
    {
        int negative = 0;
        for(int i = 0; i < _array.Length; i++)
        {
            if(_array[i] < 0)
            {
                negative++;
            }
        }

        return negative;
    }
}
