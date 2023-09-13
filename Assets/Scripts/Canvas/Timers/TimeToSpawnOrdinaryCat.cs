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
            TimeSpawnBlackCatCame?.Invoke();
            ComputationTimeToLevelUp("В дом зашел Черный кот!\nКоты стали умнее!");
        }
    }

    public override void ComputationTimeToSpawn()
    {
        _currentSpawnTime = CountDownTimeToSpawn();
    }

    public override void ComputationTimeToLevelUp(string text)
    {
        _currentTimeLevelUpSpawnEnemies = CountDownToLevelUp(text);
    }
}
