using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Vector2 _spawnPoint;
    [SerializeField] private int _startPool;

    [SerializeField] private GameObject _bulletPref;
    private Queue<GameObject> _bulletsQ;
    private bool _isLookRight = true;
    private PlayerService _playerService;

    private void Awake()
    {
        _playerService = GetComponent<PlayerService>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _bulletsQ = new Queue<GameObject>();

        for (int i = 0; i < _startPool; i++)
        {
            CreateBullet();
        }
    }

    private void Update()
    {
        _isLookRight = _playerService.MovementComponent.IsLookRight;
    }

    private void CreateBullet()
    {
        GameObject bullet = Instantiate(_bulletPref);
        bullet.transform.SetParent(transform);
        bullet.transform.localPosition = Vector2.zero;
        bullet.SetActive(false);

        _bulletsQ.Enqueue(bullet);
    }

    public Bullet GetBullet(float damage, float speed, bool isCritical = false, float timer = 3f)
    {
        if(_bulletsQ.Count == 0)
        {
            CreateBullet();
        }

        GameObject bullet = _bulletsQ.Dequeue();
        bullet.SetActive(true);
        bullet.transform.SetParent(null);
        Vector2 realSpawnPoint = transform.position;
        realSpawnPoint += _isLookRight ? _spawnPoint : new Vector2(-_spawnPoint.x, _spawnPoint.y);
        bullet.transform.position = realSpawnPoint;
        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        Vector2 direction = _isLookRight ? Vector2.right : Vector2.left;
        bulletComponent.Fire(damage, speed, direction, _isLookRight, isCritical, timer);
        bulletComponent.OnHit += ReturnBullet;

        return bulletComponent;
    }

    public void ReturnBullet(GameObject bullet)
    {
        if (_bulletsQ.Contains(bullet)) return;

        _bulletsQ.Enqueue(bullet);

        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        bulletComponent.OnHit -= ReturnBullet;

        bullet.transform.SetParent(transform);
        bullet.transform.localPosition = Vector2.zero;
        bullet.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector2 realSpawnPoint = transform.position;
        realSpawnPoint += _isLookRight ? _spawnPoint : new Vector2(-_spawnPoint.x, _spawnPoint.y);
        Gizmos.DrawWireSphere(realSpawnPoint, 0.2f);
    }
}
