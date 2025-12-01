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
    private Waypoint _lastWaypoint;
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

    public void Initialize(float speed, Waypoint nextPoint, bool isMovingForward)
    {
        _speed = speed;
        _nextPoint = nextPoint;
        _isMovingForvard = isMovingForward;

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
            Waypoint nextWaypoint = _nextPoint.GetNextWaypoint(ref _isMovingForvard, _lastWaypoint);
            _lastWaypoint = _nextPoint;
            if (nextWaypoint)
            {
                SetNextPoint(nextWaypoint);
            }
            else
            {
                WayEnds();
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

        _lastWaypoint = null;
        _isMovingForvard = !_isMovingForvard;
    }

    private void SetNextPoint(Waypoint nextPoint)
    {
        _nextPoint = nextPoint;
        _nextPosition = _nextPoint.GetPoint();
    }
}
