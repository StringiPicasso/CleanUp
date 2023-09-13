using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Transform[] _enemiesSawnPoints;
    [SerializeField] private AnimationEdit _enemyPrefab;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private TimeToSpawnEnemy _timeSpawn;

    public List<VacuumCleaner> _spawnedEnemies;

    private int _currentLevel;
    private int _currentCurrentExperience;
    private Vector3 _currentScale;
    private int _currentMaxExerienceForLevel;
    private int _randomIndex;

    public event UnityAction<List<VacuumCleaner>> SomethimgHapped;
    public event UnityAction<string> ChangeEXP;

    private void Start()
    {
        _spawnedEnemies.Add(_playerPrefab);
    }

    private void OnEnable()
    {
        _timeSpawn.FirstTimeSpawnEnemyCame += OnFirstTimeSpawnEnemyCame;
        _timeSpawn.TimeSpawnEnemyCame += OnTimeSpawnEnemyCame;
        _playerPrefab.TotalExperienceTaked += OnExperienceTaked;
        _playerPrefab.PlayerReady += OnEnemySpawned;
    }

    private void OnDisable()
    {
        _playerPrefab.PlayerReady -= OnEnemySpawned;
    }

    private void OnExperienceTaked(string name)
    {
        ChangeEXP?.Invoke(name);
    }

    private void OnEnemySpawned(VacuumCleaner vacuumCleaner)
    {
        SomethimgHapped?.Invoke(_spawnedEnemies);
    }

    private void OnFirstTimeSpawnEnemyCame(int level, int currentExp, Vector3 scale, int maxExpForLevel)
    {
        for (int i = 0; i < _enemiesSawnPoints.Length; i++)
        {
            SpawnEnemy(i, level, currentExp, scale, maxExpForLevel);
        }
    }

    private void OnTimeSpawnEnemyCame(int level, int currentExp, Vector3 scale, int maxExpForLevel)
    {
        _currentLevel = level;
        _currentCurrentExperience = currentExp;
        _currentScale = scale;
        _currentMaxExerienceForLevel = maxExpForLevel;
        _randomIndex = Random.Range(0, _enemiesSawnPoints.Length);
        OnTimeToSpawnEnemyCame();
    }

    private void OnTimeToSpawnEnemyCame()
    {
        SpawnEnemy(_randomIndex, _currentLevel, _currentCurrentExperience, _currentScale, _currentMaxExerienceForLevel);
    }

    private void SpawnEnemy(int index, int level, int currentExp, Vector3 scale, int maxExpForLevel)
    {
        var currentEnemy = InstantiateEnemy(_enemiesSawnPoints[index]);
        currentEnemy.GetComponentInChildren<Enemy>().TakeCharactersForEnemy(level, currentExp, scale, maxExpForLevel);
        _spawnedEnemies.Add(currentEnemy.GetComponentInChildren<Enemy>());
    }

    private AnimationEdit InstantiateEnemy(Transform spawnPoint)
    {
        var currentEnemy = Instantiate(_enemyPrefab, spawnPoint);
        currentEnemy.GetComponentInChildren<Enemy>().EnemySpawned += OnEnemySpawned;
        currentEnemy.GetComponentInChildren<Enemy>().EnemyDied += OnTimeToSpawnEnemyCame;
        currentEnemy.GetComponentInChildren<Enemy>().ExperienceTaked += OnExperienceTaked;

        return currentEnemy;
    }
}
