using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimeToSpawn : MonoBehaviour
{
    [SerializeField] protected float _startSpawnTime;
    [SerializeField] protected float _subtractedValueFromStartSpawnTime;
    [SerializeField] protected float _minTimeSpawn;
    [SerializeField] protected float _timeLevelUpSpawnEnemies;

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
