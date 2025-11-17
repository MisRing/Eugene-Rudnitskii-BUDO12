using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    private class PooledObject
    {
        public int StartCount = 0;
        public PooledObjectType Type;
        public GameObject Prefab;
    }

    [SerializeField] private List<PooledObject> _pooledObjects;
    private Dictionary<PooledObjectType, GameObject> _pooledPrefabs;

    private Dictionary<PooledObjectType, Queue<GameObject>> _objectQs;

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        foreach(PooledObject obj in _pooledObjects)
        {
            _pooledPrefabs[obj.Type] = obj.Prefab;
            for(int i = 0; i < obj.StartCount; i++)
            {
                CreateObject(obj.Type);
            }
        }
    }

    private void CreateObject(PooledObjectType type)
    {
        if(!_objectQs.ContainsKey(type))
        {
            _objectQs.Add(type, new Queue<GameObject>());
        }

        GameObject obj = Instantiate(_pooledPrefabs[type], transform);
        obj.SetActive(false);
        IReturnable objReturnable = obj.GetComponent<IReturnable>();
        objReturnable.Return += ReturnObject;
        _objectQs[type].Enqueue(obj);
    }

    public GameObject GetObject(PooledObjectType type, Vector3 spawnPosition, Quaternion rotation)
    {
        if (_objectQs[type].Count == 0)
        {
            CreateObject(type);
        }

        GameObject obj = _objectQs[type].Dequeue();
        obj.transform.SetParent(null);
        obj.transform.position = spawnPosition;
        obj.transform.rotation = rotation;
        obj.SetActive(true);

        return obj;
    }

    private void ReturnObject(GameObject obj, PooledObjectType type)
    {
        obj.transform.SetParent(transform);
        obj.transform.position = transform.position;
        obj.SetActive(false);
        _objectQs[type].Enqueue(obj);
    }
}

public enum PooledObjectType
{
    Bullet_Enemy,
    Bullet_Player,
    Explosion_Enemy,
    Explosion_Asteroid,
    Explosion_Player
}
