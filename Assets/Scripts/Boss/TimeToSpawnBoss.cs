using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeToSpawnBoss : MonoBehaviour
{
    [SerializeField] private float _killTime;
    [SerializeField] private float _addedToNewKillTime;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _startSpawnTime;
    [SerializeField] private float _subtractedValueFromStartSpawnTime;
    [SerializeField] private float _minTimeSpawn;
    [SerializeField] private InstantiateSpawnTextCanvas _spawnTextLevelUp;

    private float _currentSpawnTime;
    private float _newStartSpawnTime;

    public event UnityAction<float, float> TimeSpawnBossCame;

    private void Start()
    {
        _currentSpawnTime = _startSpawnTime;
        _newStartSpawnTime = _startSpawnTime;
    }

    private void Update()
    {
        _currentSpawnTime -= Time.deltaTime;

        if (_currentSpawnTime <= 0)
        {
            CountDownTimeToSpawn(TimeSpawnBossCame);
        }
    }

    protected void CountDownTimeToSpawn(UnityAction<float, float> eventName)
    {
        eventName?.Invoke(_lifeTime, _killTime);
        _newStartSpawnTime -= _subtractedValueFromStartSpawnTime;

        if (_newStartSpawnTime < _minTimeSpawn)
        {
            _spawnTextLevelUp.CreateTextOnDisplay("Босс стал сильнее!");
            _killTime += _addedToNewKillTime;
            _currentSpawnTime = _minTimeSpawn;
        }
        else
        {
            _currentSpawnTime = _newStartSpawnTime;
        }
    }

}
