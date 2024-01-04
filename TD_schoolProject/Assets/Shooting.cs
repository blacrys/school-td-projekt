using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Shooting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject bulletPrefab;
    
    [Header("Attributes")]
    [SerializeField] private float range = 5f;
    [SerializeField] private float fireRate = 1f;

    private Transform target;
    private float timeSinceLastFire;

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }
        
        if(!CheckTargetInRange())
        {
            target = null;
            return;
        }
        else
        {
            timeSinceLastFire += Time.deltaTime;
            if (timeSinceLastFire >= 1f / fireRate)
            {
                Shoot();
                timeSinceLastFire = 0f;
            } 
        }
    }
    
    private bool CheckTargetInRange()
    {
        return Vector2.Distance(transform.position, target.position) <= range;
    }

    private void Shoot()
    {
        GameObject bulletObj  = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet buletScript = bulletObj.GetComponent<Bullet>();
        buletScript.SetTarget(target);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, Vector2.zero);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, Vector3.forward, range);
    }
}