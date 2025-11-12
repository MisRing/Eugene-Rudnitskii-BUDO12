using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyComponent : ShipFlyComponent
{
    [Header("Special Fly Settings")]
    [SerializeField] private float _flyInSpeedMod = 2f;
    [SerializeField] private Vector3 _startPosition;
    private EnemyShipService _enemyShipService;

    private protected override void Awake()
    {
        base.Awake();
        _enemyShipService = GetComponent<EnemyShipService>();
    }

    private void Start()
    {
        //StartCoroutine(FlyToScreen());
        StartAI();
    }

    //private IEnumerator FlyToScreen()
    //{
        
    //    StartAI();
    //}



    private Vector3 GetMovementOnPoint(Vector3 point)
    {
        float delta;

        if (_boundary)
        {
            delta = ((transform.position.x - _boundary.MinX) / (_boundary.MaxX - _boundary.MinX)) * 2f;
        }
        else
        {
            delta = ((transform.position.x + 10f) / 20f) * 2f;
        }
        Vector3 movement = MovementPressets.GetMovementByType(delta, _enemyShipService.MovementType);

        return movement;
    }

    private void StartAI()
    {
        StartCoroutine(Fly());
    }

    private IEnumerator Fly()
    {
        float delta;

        if (_boundary)
        {
            delta = ((transform.position.x - _boundary.MinX) / (_boundary.MaxX - _boundary.MinX)) * 2f;
        }
        else
        {
            delta = ((transform.position.x + 10f) / 20f) * 2f;
        }

        while (true)
        {
            _movement = MovementPressets.GetMovementByType(delta, _enemyShipService.MovementType);

            yield return null;

            delta += Time.deltaTime / (_flySpeed / 2f);

            if (delta > 4f)
            {
                delta -= 4f;
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        if(!_enemyShipService)
        {
            _enemyShipService = GetComponent<EnemyShipService>();
        }
        Gizmos.color = Color.red;

        float step = 0.05f;
        float delta = 0f;

        Vector3 prevPoint = new Vector3(0f, 0f, _startPosition.z);
        while (delta <= 2f)
        {
            Gizmos.DrawLine(prevPoint,
                                prevPoint + MovementPressets.GetMovementByType(delta + step / 2, _enemyShipService.MovementType) * _flySpeed * step * 4);
            prevPoint += MovementPressets.GetMovementByType(delta + step / 2, _enemyShipService.MovementType) * _flySpeed * step * 4;
            delta += step;
        }
    }
}
