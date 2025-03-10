using UnityEngine;
using System.Collections;

public class Spawning : MonoBehaviour
{
    [SerializeField]
    private float _minSpawnDelay = 3f, _maxSpawnDelay = 5f, _roundDelay = 120f;
    public GameObject[] enemyNPCs;
    public GameObject[] spawnpoints;
    public int _roundOneEnemyCapacity = 20, _currentRoundEnemyCapacity;
    public int _currentRound;

    private int _enemiesSpawned;
    private float _currentSpawnCooldown, _spawningFinishedCountdown;

    private System.Random _randomInteger = new System.Random();

    void Start()
    {
        _currentRound = 1;
        _currentSpawnCooldown = 0f;
        _spawningFinishedCountdown = _roundDelay;
        _enemiesSpawned = 0;
        SetRoundEnemyCapacity();
    }

    public void SetRoundEnemyCapacity()
    {
        _currentRoundEnemyCapacity = _roundOneEnemyCapacity+(2*_currentRound);
    }

    private Vector3 PickSpawnPoint()
    {
        int index = _randomInteger.Next(0, spawnpoints.Length-1);
        while(!spawnpoints[index].activeSelf)
        {
            index = _randomInteger.Next(0, spawnpoints.Length-1);
        }
        return spawnpoints[index].transform.position;
    }

    private void SpawnEnemy()
    {
        GameObject enemy = enemyNPCs[_randomInteger.Next(0, enemyNPCs.Length-1)];
        Vector3 spawnpoint = PickSpawnPoint();
        Instantiate(enemy, spawnpoint, Quaternion.identity);
    }

    void Update()
    {
        _currentSpawnCooldown -= Time.deltaTime;
        //Controls spawning so that the correct number of enemies are spawned in when appropriate
        if(_enemiesSpawned < _currentRoundEnemyCapacity && _currentSpawnCooldown <= 0 )
        {
            SpawnEnemy();
            _enemiesSpawned++;
            _currentSpawnCooldown = Random.Range(_minSpawnDelay, _maxSpawnDelay);
        }

        //Counts 120 seconds after all enemies are spawned so that another round can be started at the end of the timer
        if(_enemiesSpawned >= _currentRoundEnemyCapacity)
        {
            _spawningFinishedCountdown -= Time.deltaTime;
        }

        //Moves the game into the next round and resetting values that need reseting
        if(_enemiesSpawned >= _currentRoundEnemyCapacity && ((GameObject.FindGameObjectsWithTag("EnemyNPC").Length == 0) || _spawningFinishedCountdown <= 0))
        {
            _currentRound++;
            _spawningFinishedCountdown = _roundDelay;
            SetRoundEnemyCapacity();
            _enemiesSpawned = 0;
        }
    }
}
