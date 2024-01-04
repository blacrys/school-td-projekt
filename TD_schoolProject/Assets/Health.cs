using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float hitPoints = 5;
    [SerializeField] private int currencyValue = 10;
    
    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        
        if (hitPoints <= 0)
        {
            EnemySpawner.OnEnemyDestroyed.Invoke();
            CurrencyManager.Main.EarnCurrency(currencyValue);
            Destroy(gameObject);
        }
    } 
}
