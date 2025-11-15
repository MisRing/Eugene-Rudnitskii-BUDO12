using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyComponent : ShipFlyComponent
{
    [Header("Special Fly Settings")]
    [SerializeField, Range(1f, 10f)] private float _flyInSpeedMod = 2f;
    [SerializeField] private float _flyInDistance = 0.3f;
    [SerializeField] private Vector3 _startPosition;
    private EnemyShipService _enemyShipService;

    private protected override void Awake()
    {
        base.Awake();
        _enemyShipService = GetComponent<EnemyShipService>();
    }

    private void Start()
    {
        StartMove(_startPosition);
    }

    public void StartMove(Vector3 startPoint)
    {
        StartCoroutine(FlyInLinear(startPoint));
        //StartAI();
    }

    private IEnumerator FlyInLinear(Vector3 startPoint)
    {
        float maxDistance = Vector3.Distance(startPoint, transform.position);
        Vector3 finalMovement = GetMovementOnPoint(startPoint);
        
        transform.position = new Vector3((transform.position.z - startPoint.z) * Mathf.Sin(Vector3.Angle(Vector3.up, finalMovement)) + startPoint.x, 0f, transform.position.z);

        float t = 1;

        while(Vector3.Distance(transform.position, startPoint) > _flyInDistance)
        {
            _movement = (startPoint - transform.position).normalized;
            _movement *= 1 + (1 - Mathf.Pow(1 - t, 2)) * (_flyInSpeedMod - 1);
            t = Vector3.Distance(startPoint, transform.position) / maxDistance;

            yield return null;
        }    

        StartAI();
    }



    private Vector3 GetMovementOnPoint(Vector3 point)
    {
        float delta;

        if (_boundary)
        {
            delta = ((point.x - _boundary.MinX) / (_boundary.MaxX - _boundary.MinX)) * 2f;
        }
        else
        {
            delta = ((point.x + 10f) / 20f) * 2f;
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
            delta = ((_startPosition.x - _boundary.MinX) / (_boundary.MaxX - _boundary.MinX)) * 2f;
        }
        else
        {
            delta = ((_startPosition.x + 10f) / 20f) * 2f;
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

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + _movement * 2);
    }
}
