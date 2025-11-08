using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private SpawnArea _spawnArea;

    [SerializeField] private List<GameObject> asteroids = new List<GameObject>();

    private void Awake()
    {
        _spawnArea = GetComponent<SpawnArea>();
    }

    float nextSpawn = 0f;
    private void Update()
    {
        if (nextSpawn <= Time.time)
        {
            GameObject asteroid = Instantiate(asteroids[Random.Range(0, asteroids.Count)]);
            asteroid.transform.position = _spawnArea.GetRandomPosition();

            nextSpawn = Time.time + 0.5f;
        }
    }
}
