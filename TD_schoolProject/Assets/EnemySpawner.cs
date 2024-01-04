using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform startPoint;

    [Header("Attributes")] 
    [SerializeField] private int baseEnemies = 10;
    [SerializeField] private float spawnRate = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float dificultyMultiplier = 0.75f;
    
    [Header("Events")]
    public static UnityEvent OnEnemyDestroyed = new UnityEvent() ;
    
    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesToSpawn;
    private bool isSpawning = false;
    
    private void Awake()
    {
        OnEnemyDestroyed.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }
    
    private void Update()
    {
        if(!isSpawning) return;
        
        timeSinceLastSpawn += Time.deltaTime;
        
        if(timeSinceLastSpawn >= (1f / spawnRate) && enemiesToSpawn > 0)
        {
            SpawnEnemy();
            enemiesToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }
        
        if (enemiesToSpawn == 0 && enemiesAlive == 0)
        {
            EndWave();
        }
    }
    
    private void SpawnEnemy() 
    {
        Instantiate(enemyPrefab, startPoint.position, Quaternion.identity);
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }
    
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        
        isSpawning = true;
        enemiesToSpawn = EnemiesPerWave(); 
    }
    
    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    } 
    
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, dificultyMultiplier));
    }
}
