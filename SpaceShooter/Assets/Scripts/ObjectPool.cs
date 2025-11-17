using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int _minCount;
    [SerializeField] private GameObject _objectPref;

    private Queue<GameObject> _objectsQ = new Queue<GameObject>();

    private void Start()
    {
        for(int i = 0; i < _minCount; i++)
        {
            CreateObject();
        }
    }

    private void CreateObject()
    {
        GameObject obj = Instantiate(_objectPref, transform);
        obj.SetActive(false);
        IReturnable objReturnable = obj.GetComponent<IReturnable>();
        objReturnable.Return += ReturnObject;
        _objectsQ.Enqueue(obj);
    }

    public GameObject GetObject(Vector3 spawnPosition, Quaternion rotation)
    {
        if(_objectsQ.Count == 0)
        {
            CreateObject();
        }

        GameObject obj = _objectsQ.Dequeue();
        obj.transform.SetParent(null);
        obj.transform.position = spawnPosition;
        obj.transform.rotation = rotation;
        obj.SetActive(true);

        return obj;
    }

    private void ReturnObject(GameObject obj)
    {
        obj.transform.SetParent(transform);
        obj.transform.position = transform.position;
        obj.SetActive(false);
        _objectsQ.Enqueue(obj);
    }
}
