using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    
    [Header("Attributes")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 1f;
    
    private Transform target;
    
    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

    private void FixedUpdate()
    {
        if (!target)
        {
            Destroy(gameObject);
            return;
        }
         
        Vector2 direction = (target.position - transform.position).normalized;
        
        rb.velocity = direction * speed;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<Health>().TakeDamage(damage);
        Destroy(gameObject); 
    }
}
