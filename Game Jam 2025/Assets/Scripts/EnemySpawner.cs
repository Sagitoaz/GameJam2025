using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int baseEnemy = 8;
    [SerializeField] private float enemyPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWave = 30f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    public static UnityEvent onEnemyDestroy = new UnityEvent();
    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroy);
    }
    void Start()
    {
        StartCoroutine(StartWave());
    }
    void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= (1.0f / enemyPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }
    private void EnemyDestroy()
    {
        enemiesAlive--;
    }
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWave);
        isSpawning = true;
        enemiesLeftToSpawn = baseEnemy;
    }
    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        if (currentWave > 3) return;
        StartCoroutine(StartWave());
    }
    private void SpawnEnemy()
    {
        int typeOfEnemy = Random.Range(0, currentWave);
        GameObject prefabToSpawn = enemyPrefabs[typeOfEnemy];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }
    private int EnemyPerWave()
    {
        return Mathf.RoundToInt(baseEnemy * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}
