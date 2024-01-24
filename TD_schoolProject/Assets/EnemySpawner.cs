using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform startPoint;

    [Header("Attributes")] 
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float spawnRate = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float dificultyMultiplier = 0.75f;
    [SerializeField] private float enemiesPerSecondMax = 15f;
    
    [Header("Events")]
    public static UnityEvent OnEnemyDestroyed = new UnityEvent() ;

    public int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesToSpawn;
    private float eps; //enemy per second
    private bool isSpawning = false;
    
    public static EnemySpawner Main;
    
    private void Awake()
    {
        OnEnemyDestroyed.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
        Main = GetComponent<EnemySpawner>();
    }
    
    private void Update()
    {
        if(!isSpawning) return;
        
        timeSinceLastSpawn += Time.deltaTime;
        
        if(timeSinceLastSpawn >= (1f / eps) && enemiesToSpawn > 0)
        {
            SpawnEnemy();
            enemiesToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }
        
        if (enemiesToSpawn == 0 && enemiesAlive <= 0)
        {
            EndWave();
        }
    }
    
    private void SpawnEnemy() 
    {
        int index = UnityEngine.Random.Range(0, enemyPrefabs.Length);
        (enemyPrefabs[index].GetComponent<Health>()).UpgradeHealth(currentWave, dificultyMultiplier);
        Instantiate(enemyPrefabs[index], startPoint.position, Quaternion.identity);
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
        eps = spawnRate;
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
    
    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(spawnRate * Mathf.Pow(currentWave, dificultyMultiplier), 0f, enemiesPerSecondMax);
    }
}
