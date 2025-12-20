using UnityEngine;

public class CharacterNavigationController : MonoBehaviour
{
    [Header("Navigation Settings")]
    [SerializeField] private float _minDistance = 0.1f;
    [SerializeField] private bool _isWaitOnEnd = true;
    [SerializeField] private float _minWaitTime = 1f, _maxWaitTime = 5f;
    private float _waitEnds = 0f;

    private Waypoint _nextPoint;
    private Waypoint _lastWaypoint;

    private CharacterService _characterService;

    public void Initialize(Waypoint nextPoint, bool isWaitOnEnd, CharacterService characterService)
    {
        _characterService = characterService;

        _nextPoint = nextPoint;
        _isWaitOnEnd = isWaitOnEnd;

        if (!_nextPoint) return;

        _characterService.CharacterMovementComponent.TargetPosition = _nextPoint.GetPointPosition();
        _characterService.CharacterMovementComponent.IsMoving = true;
    }

    private void LateUpdate()
    {
        CheckWay();
    }

    private void CheckWay()
    {
        if (!_characterService.CharacterMovementComponent.IsMoving)
        {
            if (_waitEnds <= Time.time)
            {
                SetNextPoint();
            }
            else return;
        }

        if (Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), _characterService.CharacterMovementComponent.TargetPosition) < _minDistance)
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
                _characterService.CharacterMovementComponent.IsMoving = false;
                _waitEnds = Time.time + Random.Range(_minWaitTime, _maxWaitTime);
                return;
            }
            _lastWaypoint = null;
            nextWaypoint = _nextPoint.ConnectionComponent.GetNextWaypoint(_lastWaypoint);
        }

        _nextPoint = nextWaypoint;
        _characterService.CharacterMovementComponent.TargetPosition = _nextPoint.GetPointPosition();
        _characterService.CharacterMovementComponent.IsMoving = true;
    }
}
