using UnityEngine;
using Characters.Player;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _maxGap = 1f;
    [SerializeField, Range(0, 1)] private float _minSpeedPercent = 0.8f;

    private Rigidbody2D _targetRigidbody;
    private PlayerService _targetPlayerService;

    private void Awake()
    {
        _targetRigidbody = _target.GetComponent<Rigidbody2D>();
        _targetPlayerService = _target.GetComponent<PlayerService>();
    }

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        float targetSpeed = _targetRigidbody.velocity.magnitude;
        float gap = Vector2.Distance(_target.position, transform.position);

        float followSpeed = gap / _maxGap * targetSpeed;
        float minSpeed = _targetPlayerService.Stats.MoveSpeed.Value * _minSpeedPercent;
        followSpeed = Mathf.Clamp(followSpeed, minSpeed, float.MaxValue);

        transform.position = Vector2.Lerp(transform.position, _target.position, followSpeed * Time.deltaTime);
    }
}
