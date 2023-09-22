using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeToSpawnEnemy : TimeToSpawn
{
    [SerializeField] private AnimationEdit _playerPrefab;
    [SerializeField] private EnemySpawn _enemySpawn;
    [SerializeField] private float _subtractedToLowFromDiedEnemy;
    [SerializeField] private InstantiateSpawnTextCanvas _spawnTextLevelUp;

    private int _currentLevel;
    private int _currentCurrentExperience;
    private Vector3 _currentScale;
    private int _currentMaxExerienceForLevel;

    public event UnityAction<int, int, Vector3, int> TimeSpawnEnemyCame;
    public event UnityAction<int, int, Vector3, int> FirstTimeSpawnEnemyCame;

    private void Start()
    {
        _enemySpawn.SomeEnemyDied += OnSomeEnemyDied;
        _currentLevel = _playerPrefab.GetComponentInChildren<Player>().Level;
        _currentCurrentExperience = 0;
        _currentScale = _playerPrefab.ScalePlayer;
        _currentMaxExerienceForLevel = _playerPrefab.GetComponentInChildren<Player>().MaxExperienceForLevel;

        FirstTimeSpawnEnemyCame?.Invoke(_currentLevel, _currentCurrentExperience, _currentScale, _currentMaxExerienceForLevel);
    }

    private void Update()
    {
        _currentSpawnTime -= Time.deltaTime;

        if (_currentSpawnTime <= 0)
        {
            TimeSpawnEnemyCame?.Invoke(_currentLevel, _currentCurrentExperience, _currentScale, _currentMaxExerienceForLevel);

            ComputationTimeToSpawn();
        }

        _currentTimeLevelUpSpawnEnemies -= Time.deltaTime;

        if (_currentTimeLevelUpSpawnEnemies <= 0)
        {
            _currentLevel = _playerPrefab.GetComponentInChildren<Player>().Level + 1;
            _currentMaxExerienceForLevel = _playerPrefab.GetComponentInChildren<Player>().MaxExperienceForLevel + 20;
            _currentCurrentExperience = Random.Range(0, _currentMaxExerienceForLevel);
            _currentScale = _playerPrefab.ScalePlayer + _playerPrefab.NumberNewLocalScaleAdded;
            _currentTimeLevelUpSpawnEnemies = _timeLevelUpSpawnEnemies;

            TimeSpawnEnemyCame?.Invoke(_currentLevel, _currentCurrentExperience, _currentScale, _currentMaxExerienceForLevel);
            _spawnTextLevelUp.CreateTextOnDisplay("Появился сильный противник!");
        }
    }

    private void OnSomeEnemyDied()
    {
        _currentSpawnTime -= _subtractedToLowFromDiedEnemy;
    }

    public override void ComputationTimeToSpawn()
    {
        _currentSpawnTime = CountDownTimeToSpawn();
    }
}
