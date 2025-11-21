using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnArea))]
[RequireComponent(typeof(EnemyFactory))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _startTime = 3f;
    [SerializeField] private float _breakeTime = 3f;
    [SerializeField] private List<Round> _rounds;

    [SerializeField] private ObjectPool _objectPool;

    [System.Serializable]
    public class Round
    {
        public WaveType Type;
        public float Period = 0.5f;
        [Range(1, 10)]
        public int Iterations = 1;

        public List<EnemyData> enemyDatas;

        public enum WaveType
        {
            Period,
            AtTheSameTime
        }
    }

    private SpawnArea _spawnArea;
    private EnemyFactory _enemyFactory;
    
    private void Awake()
    {
        _spawnArea = GetComponent<SpawnArea>();
        _enemyFactory = GetComponent<EnemyFactory>();
    }

    private void Start()
    {
        if (_rounds.Count == 0) return;

        StartCoroutine(PlayRound(0));
    }

    private IEnumerator PlayRound(int index, float startDelay = 0)
    {
        yield return new WaitForSeconds(startDelay);

        Round round = _rounds[index];

        if(round.Type == Round.WaveType.AtTheSameTime)
        {
            for(int i = 0; i < round.Iterations; i++)
            {
                for (int j = 0; j < round.enemyDatas.Count; j++)
                {
                    SpawnEnemy(round.enemyDatas[j]);
                }
            }
        }
        else
        {
            for (int i = 0; i < round.Iterations; i++)
            {
                for (int j = 0; j < round.enemyDatas.Count; j++)
                {
                    SpawnEnemy(round.enemyDatas[j]);
                    yield return new WaitForSeconds(round.Period);
                }
            }
        }

        yield return new WaitForSeconds(_breakeTime);
    }

    private void SpawnEnemy(EnemyData data)
    {
        GameObject enemy = Instantiate(data.Prefab);
        enemy.transform.position = _spawnArea.GetRandomPosition();
        enemy.GetComponent<IEnemy>().Initialize(data, _objectPool);
    }
}
