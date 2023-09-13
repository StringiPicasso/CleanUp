using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class TimeToSpawn : MonoBehaviour
{
    [SerializeField] protected float _startSpawnTime;
    [SerializeField] protected float _subtractedValueFromStartSpawnTime;
    [SerializeField] protected float _subtractedToLowMinTimeSpawn;
    [SerializeField] protected float _minTimeSpawn;
    [SerializeField] protected float _timeLevelUpSpawnEnemies;
    [SerializeField] private InstantiateSpawnTextCanvas _spawnTextLevelUp;

    protected float _currentTimeLevelUpSpawnEnemies;
    protected float _currentSpawnTime;
    protected float _newStartSpawnTime;

    private void Awake()
    {
        _currentTimeLevelUpSpawnEnemies = _timeLevelUpSpawnEnemies;
        _currentSpawnTime = _startSpawnTime;
        _newStartSpawnTime = _startSpawnTime;
    }

    public abstract void ComputationTimeToSpawn();
    public abstract void ComputationTimeToLevelUp(string text);

    protected float CountDownToLevelUp(string text)
    {
        _spawnTextLevelUp.CreateTextOnDisplay(text);
        _minTimeSpawn -= _subtractedToLowMinTimeSpawn;

        return _timeLevelUpSpawnEnemies;
    }

    protected float CountDownTimeToSpawn()
    {
        _newStartSpawnTime -= _subtractedValueFromStartSpawnTime;

        if (_newStartSpawnTime <= _minTimeSpawn)
        {
            return _minTimeSpawn;
        }
        else
        {
            return _newStartSpawnTime;
        }
    }
}
