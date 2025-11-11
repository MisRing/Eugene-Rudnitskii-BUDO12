using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyPrefabs = new List<GameObject>();

    public IAsteroid CreateEnemy(EnemyType type, Vector3 position)
    {
        GameObject enemyGameObject = null;

        switch (type)
        {
            case EnemyType.asteroid0:
                enemyGameObject = Instantiate(_enemyPrefabs[0], position, Quaternion.identity);
                break;
            case EnemyType.asteroid1:
                enemyGameObject = Instantiate(_enemyPrefabs[1], position, Quaternion.identity);
                break;
            case EnemyType.asteroid2:
                enemyGameObject = Instantiate(_enemyPrefabs[2], position, Quaternion.identity);
                break;
            default:
                Debug.LogError("Unknown enemy type!");
                return null;
        }

        IAsteroid asteroid = enemyGameObject.GetComponent<IAsteroid>();
        if (asteroid != null)
        {
            asteroid.Initialize(new AsteroidData { Size = 2, Speed = 3f }); 
        }
        return asteroid;
    }
}

public enum EnemyType
{
    asteroid0,
    asteroid1,
    asteroid2,
    ship,
    bossShip
}
