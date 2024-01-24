using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float hitPoints = 5;
    [SerializeField] private int currencyValue = 10;
    
    public float hp = 3;

    
    private void Awake()
    {
        hp = GetComponent<Health>().hp;
    }
    
    
    public void UpgradeHealth(float currentWave, float dificultyMultiplier)
    {
        hp = hitPoints * Mathf.Pow(currentWave, dificultyMultiplier);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        
        if (hp <= 0)
        {
            EnemySpawner.OnEnemyDestroyed.Invoke();
            CurrencyManager.Main.EarnCurrency(currencyValue);
            Destroy(gameObject);
        }
    } 
}
