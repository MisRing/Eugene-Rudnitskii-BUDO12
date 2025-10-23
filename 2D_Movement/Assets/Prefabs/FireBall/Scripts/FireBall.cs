using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FireBall : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _lifetime = 3f;
    private Rigidbody2D _rb;

    public delegate void OnHit(GameObject bullet);
    public OnHit OnHitVoid;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.isKinematic = true;
    }
    
    private void OnEnable()
    {
        StartCoroutine(LifeTime());
        _rb.velocity = transform.right * _speed;
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(_lifetime);
        OnHitVoid(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
            return;
        if (collision.isTrigger)
            return;
        if (collision.gameObject.tag == "Player")
            return;

        OnHitVoid(gameObject);
    }
}
