using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private DamageData _damageData;
    private float _speed;
    private bool _isLookRight;
    [SerializeField]
    private LayerMask _targetLayers;

    public event Action<GameObject> OnHit;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.isKinematic = true;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Fire(DamageData damageData, float speed, bool isLookRight, float timer)
    {
        _damageData = damageData;
        _isLookRight = isLookRight;
        _speed = speed;

        Flip();

        _rb.velocity = damageData.Direction * _speed;

        StartCoroutine(Timer(timer));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger) return;
        if ((_targetLayers & (1 << col.gameObject.layer)) == 0) return;

        IDamageable target = col.gameObject.GetComponent<IDamageable>();

        if(target != null)
        {
            target.GetHit(_damageData, col.ClosestPoint(transform.position));
        }

        StopAllCoroutines();
        OnHit?.Invoke(gameObject);
    }

    private IEnumerator Timer(float timer)
    {
        yield return new WaitForSeconds(timer);
        OnHit?.Invoke(gameObject);
    }

    private void Flip()
    {
        _spriteRenderer.flipX = !_isLookRight;
    }
}
