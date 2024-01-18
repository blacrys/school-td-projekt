using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    
    [Header("Attributes")]
    [SerializeField] public float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;
    private float baseSpeed;

    private void Start()
    {
        baseSpeed = moveSpeed;
        target = path.main.pathPoints[pathIndex];
    }

    private void FixedUpdate()
    {
        if(Vector2.Distance(target.position, transform.position) <= 0.05f)
        {
            pathIndex++;
            
            if (pathIndex == path.main.pathPoints.Length)
            {
                EnemySpawner.OnEnemyDestroyed.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = path.main.pathPoints[pathIndex];
            }
        }
        
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = moveSpeed * newSpeed;
    }

    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }
}
