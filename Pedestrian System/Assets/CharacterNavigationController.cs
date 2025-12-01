using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody))]
public class CharacterNavigationController : MonoBehaviour
{
    [Header("Main Settings")]
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _rotationSpeed = 1f;

    [Header("Navigation Settings")]
    [SerializeField] private Waypoint _nextPoint;
    [SerializeField] private Vector3 _nextPosition;
    [SerializeField] private float _minDistance = 0.1f;
    [SerializeField] private bool _isMovingForvard = true;
    [SerializeField] private bool _isWaitOnEnd = true;
    [SerializeField] private float _minWaitTime = 1f, _maxWaitTime = 5f;
    private bool _isWaiting = false;
    private float _waitEnds = 0f;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (!_nextPoint) return;

        SetNextPoint(_nextPoint);
    }

    public void Update()
    {
        CheckWay();
    }

    private void FixedUpdate()
    {
        if (_isWaiting) return;

        _rb.linearVelocity = transform.forward * _speed;

        Vector3 direction = (_nextPosition - new Vector3(transform.position.x, 0f, transform.position.z)).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime));
    }

    private void CheckWay()
    {
        if (_isWaiting)
        {
            if (_waitEnds <= Time.time)
            {
                _isWaiting = false;
            }
            else return;
        }

        if (Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), _nextPosition) < _minDistance)
        {
            if (_isMovingForvard)
            {
                if (_nextPoint.NextPoint)
                {
                    SetNextPoint(_nextPoint.NextPoint);
                }
                else
                {
                    WayEnds();
                }
            }
            else
            {
                if (_nextPoint.PrevPoint)
                {
                    SetNextPoint(_nextPoint.PrevPoint);
                }
                else
                {
                    WayEnds();
                }
            }
        }
    }

    private void WayEnds()
    {
        if(_isWaitOnEnd)
        {
            _isWaiting = true;
            _waitEnds = Time.time + Random.Range(_minWaitTime, _maxWaitTime);
        }

        _isMovingForvard = !_isMovingForvard;
    }

    private void SetNextPoint(Waypoint nextPoint)
    {
        _nextPoint = nextPoint;
        _nextPosition = _nextPoint.GetPoint();
    }
}
