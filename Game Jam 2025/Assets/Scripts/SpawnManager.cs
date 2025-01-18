using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private int _waves = 3;
    private int _currentWave = 1;
    private int _numberOfEnemies = 8;
    private bool _stopSpawn = false;
    [SerializeField] private float _spawnTime = 3.0f;
    private float _nextTimeSpawn;
    [SerializeField] private GameObject[] _enemiesPrefabs;
    void Start()
    {
        NextWaveButton.isButtonClicked = true;
    }
    void Update()
    {
        if (!_stopSpawn)
        {
            StartSpawn();
        }
    }
    private void StartSpawn()
    {
        if (Time.time > _nextTimeSpawn)
        {
            if (_numberOfEnemies > 0)
            {
                SpawnEnemy();
            }
            else
            {
                if (_currentWave < _waves)
                {
                    _currentWave++;
                    _numberOfEnemies = Mathf.RoundToInt(8 * Mathf.Pow(_currentWave, 0.5f));
                }
                else 
                {
                    _stopSpawn = true;
                }
            }
        }
    }
    private void SpawnEnemy()
    {
        _nextTimeSpawn = Time.time + _spawnTime;
        int typeOfEnemy = Random.Range(0, _currentWave);
        Debug.Log(typeOfEnemy);
        Instantiate(_enemiesPrefabs[typeOfEnemy], MapGenerator.startTile.transform.position, Quaternion.identity);
        _numberOfEnemies--;
    }
}
