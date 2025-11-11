using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IEnemy
{
    private Rigidbody _rb;
    [SerializeField] private float _speed = 0.5f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Initialize(EnemyData data)
    {
        _speed = data.Speed;
        transform.localScale = Vector3.one * data.Size;
        
        StartMove();
    }

    private void StartMove()
    {
        _speed = Random.Range(0.5f * _speed, 3 * _speed);
        _rb.velocity = (Vector3.back + new Vector3(Random.Range(-0.3f, 0.3f), 0f, 0f)) * _speed;

        _rb.angularVelocity = Random.onUnitSphere * Random.Range(0.1f, 3f);
    }
}
