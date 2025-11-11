using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnArea))]
[RequireComponent(typeof(EnemyFactory))]
public class EnemySpawner : MonoBehaviour
{
    private SpawnArea _spawnArea;
    private EnemyFactory _enemyFactory;
    
    private void Awake()
    {
        _spawnArea = GetComponent<SpawnArea>();
        _enemyFactory = GetComponent<EnemyFactory>();
    }

    float _nextSpawn = 0f;
    private void Update()
    {
        if (_nextSpawn <= Time.time)
        {
            
            IEnemy enemy = _enemyFactory.CreateEnemy(EnemyType.asteroid0, _spawnArea.GetRandomPosition());

            _nextSpawn = Time.time + 1.5f;
        }
    }
}
