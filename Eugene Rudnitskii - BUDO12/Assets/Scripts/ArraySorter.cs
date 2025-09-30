using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArraySorter : MonoBehaviour
{
    [SerializeField] private int[] array;

    void Start()
    {
        array = new int[30];

        for(int i = 0; i < array.Length; i++)
        {
            array[i] = Random.Range(-30, 30);
        }
        Debug.Log($"Array: {string.Join(", ", array)}");

        array = Sort(array);

        Debug.Log($"Sorted array: {string.Join(", ", array)}");
    }

    private int[] Sort(int[] _array)
    {
        int temp;

        for(int i = 0; i < _array.Length - 1; i++)
        {
            for(int j = 0; j < _array.Length - i - 1; j++)
            {
                if(_array[j] > _array[j + 1])
                {
                    temp = _array[j];
                    _array[j] = _array[j + 1];
                    _array[j + 1] = temp;
                }
            }
        }

        return _array;
    }
}
