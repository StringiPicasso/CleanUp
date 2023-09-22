using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeToSpawnOrdinaryCat : TimeToSpawn
{
    public event UnityAction TimeSpawnPetCame;
    public event UnityAction TimeSpawnBlackCatCame;

    private void Update()
    {
        _currentSpawnTime -= Time.deltaTime;

        if (_currentSpawnTime <= 0)
        {
            TimeSpawnPetCame?.Invoke();
            ComputationTimeToSpawn();
        }

        _currentTimeLevelUpSpawnEnemies -= Time.deltaTime;

        if (_currentTimeLevelUpSpawnEnemies <= 0)
        {
            _currentTimeLevelUpSpawnEnemies = _timeLevelUpSpawnEnemies;
            TimeSpawnBlackCatCame?.Invoke();
        }
    }

    public override void ComputationTimeToSpawn()
    {
        _currentSpawnTime = CountDownTimeToSpawn();
    }
}
