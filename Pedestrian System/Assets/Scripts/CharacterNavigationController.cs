using UnityEngine;

[RequireComponent(typeof(CharacterMovementComponent))]
public class CharacterNavigationController : MonoBehaviour
{
    [Header("Navigation Settings")]
    [SerializeField] private float _minDistance = 0.1f;
    [SerializeField] private bool _isWaitOnEnd = true;
    [SerializeField] private float _minWaitTime = 1f, _maxWaitTime = 5f;
    private float _waitEnds = 0f;

    private CharacterMovementComponent _characterController;

    private Waypoint _nextPoint;
    private Waypoint _lastWaypoint;

    private void Awake()
    {
        _characterController = GetComponent<CharacterMovementComponent>();
    }

    public void Initialize(float speed, Waypoint nextPoint, bool isWaitOnEnd)
    {
        _characterController.Initialize(speed);
        _nextPoint = nextPoint;
        _isWaitOnEnd = isWaitOnEnd;

        if (!_nextPoint) return;

        _characterController.TargetPosition = _nextPoint.GetPointPosition();
        _characterController.IsMoving = true;
    }

    private void LateUpdate()
    {
        CheckWay();
    }

    private void CheckWay()
    {
        if (!_characterController.IsMoving)
        {
            if (_waitEnds <= Time.time)
            {
                SetNextPoint();
            }
            else return;
        }

        if (Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), _characterController.TargetPosition) < _minDistance)
        {
            SetNextPoint();
        }
    }

    private void SetNextPoint()
    {
        Waypoint nextWaypoint = _nextPoint.ConnectionComponent.GetNextWaypoint(_lastWaypoint);
        _lastWaypoint = _nextPoint;
        if (nextWaypoint == null)
        {
            if (_isWaitOnEnd)
            {
                _characterController.IsMoving = false;
                _waitEnds = Time.time + Random.Range(_minWaitTime, _maxWaitTime);
                return;
            }
            _lastWaypoint = null;
            nextWaypoint = _nextPoint.ConnectionComponent.GetNextWaypoint(_lastWaypoint);
        }

        _nextPoint = nextWaypoint;
        _characterController.TargetPosition = _nextPoint.GetPointPosition();
        _characterController.IsMoving = true;
    }
}
