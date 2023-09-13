using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    [SerializeField] private Transform[] _bossSpawnPoints;
    [SerializeField] private Boss _bossPrefab;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private TimeToSpawnBoss _timeSpawn;
    [SerializeField] private InstantiateSpawnTextCanvas _spawntext;

    private int _randomIndex;

    private void OnEnable()
    {
        _timeSpawn.TimeSpawnBossCame+= OnTimeSpawnBossCame;
    }

    private void OnDisable()
    {
        _timeSpawn.TimeSpawnBossCame-= OnTimeSpawnBossCame;
    }

    private void OnTimeSpawnBossCame(float life,float kill)
    {
        _randomIndex = Random.Range(0, _bossSpawnPoints.Length);
        var currentBoss = Instantiate(_bossPrefab, _bossSpawnPoints[_randomIndex]);
        currentBoss.ChangeBossCaracters(life, kill);
        currentBoss.TimeKillCame += OnTimeKillCame;
        _spawntext.CreateTextOnDisplay("Появился Босс!");
    }

    private void OnTimeKillCame()
    {
        _spawntext.CreateTextOnDisplay("Босс стал агрессивнее!");
    }
}
