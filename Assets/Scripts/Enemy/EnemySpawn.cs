using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Transform[] _enemiesSawnPoints;
    [SerializeField] private AnimationEdit _enemyPrefab;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Timer _timeSpawn;

    private int _randomIndex;
    public List<VacuumCleaner> _spawnedEnemies;

    public event UnityAction <List<VacuumCleaner>> SomethimgHapped;
    public event UnityAction ChangeEXP;

    private void Start()
    {
        _spawnedEnemies.Add(_playerPrefab);

        for (int i = 0; i < _enemiesSawnPoints.Length; i++)
        {
            var currentEnemy = Instantiate(_enemyPrefab, _enemiesSawnPoints[i]);
            currentEnemy.GetComponentInChildren<Enemy>().EnemySpawned += OnEnemySpawned;
            currentEnemy.GetComponentInChildren<Enemy>().ExperienceTaked += OnExperienceTaked;
            _spawnedEnemies.Add(currentEnemy.GetComponentInChildren<Enemy>());
        }
    }

    private void OnEnable()
    {
        _timeSpawn.TimeSpawnEnemyCame += OnTimeSpawnEnemyCame;
        _playerPrefab.TotalExperienceTaked += OnExperienceTaked;
        _playerPrefab.PlayerReady += OnEnemySpawned;
    }

    private void OnDisable()
    {
        _playerPrefab.PlayerReady -= OnEnemySpawned;
    }

    private void OnExperienceTaked()
    {
        ChangeEXP?.Invoke();
    }

    public void OnEnemySpawned(VacuumCleaner vacuumCleaner)
    {
        SomethimgHapped?.Invoke(_spawnedEnemies);
    }

    private void OnTimeSpawnEnemyCame()
    {
        _randomIndex = Random.Range(0, _enemiesSawnPoints.Length);
        var currentEnemy = Instantiate(_enemyPrefab, _enemiesSawnPoints[_randomIndex]);
        currentEnemy.GetComponentInChildren<Enemy>().EnemySpawned += OnEnemySpawned;
        currentEnemy.GetComponentInChildren<Enemy>().ExperienceTaked += OnExperienceTaked;
        _spawnedEnemies.Add(currentEnemy.GetComponentInChildren<Enemy>());
    }
}
