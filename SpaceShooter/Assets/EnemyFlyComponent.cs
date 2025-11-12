using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyComponent : ShipFlyComponent
{
    private EnemyShipService _enemyShipService;

    private protected override void Awake()
    {
        base.Awake();
        _enemyShipService = GetComponent<EnemyShipService>();
    }

    private void Start()
    {
        StartCoroutine(Fly());
    }

    private IEnumerator Fly()
    {
        float delta;

        if (_boundary)
        {
            delta = (transform.position.x / _boundary.MaxX) * 90f;
        }
        else
        {
            delta = (transform.position.x / 10f) * 90f;
        }

        delta += 90f;

        while (true)
        {
            _movement = MovementPressets.GetMovementByType(delta, _enemyShipService.MovementType);
            yield return null;
            delta += Time.deltaTime / Mathf.Deg2Rad;
        }

    }
}
