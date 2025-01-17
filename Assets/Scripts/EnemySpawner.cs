using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Spawner
    [SerializeField]
    private List<GameObject> spawnerGameObjects;

    // EnemyPrefab
    [SerializeField]
    private GameObject enemyPrefab;

    // Enemy speed definitions
    [SerializeField]
    private float enemyStartSpeed = 3f;
    [SerializeField]
    private float enemySpeedIncreasePerInterval = 0.5f;
    [SerializeField]
    private float maxEnemySpeed = 5.5f;

    // Enemy spawn cooldown definitions
    [SerializeField]
    private float startSpawnCooldown = 2f;
    [SerializeField]
    private float spawnCooldownReduction = 0.25f;
    [SerializeField]
    private float minSpawnCooldown = 0.25f;

    // Number of Enemies definitions
    [SerializeField]
    private int maxEnemies = 50;

    private int spawnerCount;
    private bool running = false;
    private bool spawning = false;
    private float spawnCooldown;
    private float enemySpeed;
    private List<GameObject> enemies;

    void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        spawnerCount = spawnerGameObjects.Count;
        enemies = new List<GameObject>();
    }
    void Start()
    {
        GameManager.Instance.OnPlayerDeath += OnGameplayInterrupt;
        GameManager.Instance.OnStartGameplay += OnGameplayStart;
    }

    void Update()
    {
        if(running && spawning && enemies.Count < maxEnemies)
        {
            SpawnEnemy();
        }
    }

    private void OnGameplayInterrupt()
    {
        running = false;
        spawning = false;
    }

    private void OnGameplayStart()
    {
        spawnCooldown = startSpawnCooldown;
        enemySpeed = enemyStartSpeed;
        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        enemies.Clear();
        StartCoroutine(InitSpawnCooldown());
        running = true;
    }

    private void SpawnEnemy()
    {
        spawning = false;
        int rnd = Random.Range(0, spawnerCount - 1);
        GameObject enemyGO = Instantiate(enemyPrefab, spawnerGameObjects[rnd].transform.position, gameObject.transform.rotation);
        enemies.Add(enemyGO);
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        enemy.SetSpeed(enemySpeed);
        StartCoroutine(InitSpawnCooldown());
    }

    private IEnumerator InitSpawnCooldown()
    {
        yield return new WaitForSeconds(spawnCooldown);
        spawning = false;
        if (running)
        {
            spawning = true;
        }
    }

    public void IncreaseDifficulty()
    {
        if (running && spawnCooldown > minSpawnCooldown)
        {
            spawnCooldown -= spawnCooldownReduction;
        }
        else if (running && spawnCooldown <= minSpawnCooldown && enemySpeed < maxEnemySpeed)
        {
            spawnCooldown = 1f;
            enemySpeed += enemySpeedIncreasePerInterval;
        }
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerDeath -= OnGameplayInterrupt;
        GameManager.Instance.OnStartGameplay -= OnGameplayStart;
    }


}
