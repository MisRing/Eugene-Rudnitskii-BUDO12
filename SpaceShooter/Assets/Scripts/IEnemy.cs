using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void Initialize(EnemyData data);
}

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public int Size;
    [SerializeField] private float _minSpeed = 0.1f;
    [SerializeField] private float _maxSpeed = 0.3f;
    public float Speed => Random.Range(_minSpeed, _maxSpeed);
}