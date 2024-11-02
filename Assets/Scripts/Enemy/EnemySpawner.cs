using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;


    public event Action onEnemySpawned;
    public event Action onEnemyDespawned;

    [SerializeField] private GameObject [] enemyPrefab;
    [SerializeField] private int totalEnemyPerMin = 1000;

    private List<GameObject> enemyPool;

    private float spawnInterval = 1f / 16f;
    private float counter;
    private int totalEnemy;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        totalEnemy = totalEnemyPerMin * GameManager.Instance.GetGameLength();

        enemyPool = new List<GameObject>();
        for (int i = 0; i < totalEnemy; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab[UnityEngine.Random.Range(0, enemyPrefab.Length)]);
            enemy.GetComponent<CapsuleCollider2D>().enabled = false;
            enemy.SetActive(false);
            onEnemyDespawned?.Invoke();
            enemyPool.Add(enemy);
        }
    }

    private void Update()
    {
        counter += Time.deltaTime;
        if (counter >= spawnInterval)
        {
            SpawnEnemy();
            onEnemySpawned?.Invoke();
            counter = 0f;
        }
    }

    private void SpawnEnemy()
    {
        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.transform.position = GetRandomSpawnPosition();
                enemy.SetActive(true);
                break;
            }
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 playerPosition = Player.Instance.transform.position;

        float offsetX = UnityEngine.Random.Range(-100f, 100f);
        float offsetY = UnityEngine.Random.Range(-100f, 100f);

        return new Vector3(playerPosition.x + offsetX, playerPosition.y + offsetY, 0);
    }

}

