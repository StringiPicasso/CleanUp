using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleSpawn : MonoBehaviour
{
    [SerializeField] private Pet _petPrefab;
    [SerializeField] private Puddle _puddlePrefab;
    [SerializeField] private float _minTimeSpawnPuddle;
    [SerializeField] private float _newLfePuddleAdded;
    [SerializeField] private int _addedNewDamageMax;
    [SerializeField] private int _addedNewDamageMin;

    private int _maxDamagePuddle;
    private int _minDamagePuddle;
    private float _currentTimeSpawn;

    void Start()
    {
        _maxDamagePuddle = _puddlePrefab.CurrentMaxDamagePuddle;
        _minDamagePuddle = _puddlePrefab.CurrentMinDamagePuddle;
        _currentTimeSpawn = _petPrefab.TimeToSpawnPuddle;
    }

    void Update()
    {
        _currentTimeSpawn -= Time.deltaTime;

        if (_currentTimeSpawn <= 0)
        {
            SpawnNEwPuddle();
        }
    }

    private void SpawnNEwPuddle()
    {
        Vector3 spawnPosition =new Vector3(_petPrefab.transform.position.x, 0, _petPrefab.transform.position.z);
        var currentPuddle=Instantiate(_puddlePrefab, spawnPosition, Quaternion.identity);
        _petPrefab.SoundSpawnPuddle();
        currentPuddle.NewLevelPuddle(_newLfePuddleAdded,_maxDamagePuddle,_minDamagePuddle);
        _currentTimeSpawn =Random.Range( _minTimeSpawnPuddle,_petPrefab.TimeToSpawnPuddle);
    }

    public void OnStrongerCatSpawnrd()
    {
         _maxDamagePuddle = _puddlePrefab.CurrentMaxDamagePuddle+_addedNewDamageMax;
          _minDamagePuddle = _puddlePrefab.CurrentMinDamagePuddle+_addedNewDamageMin;
        _puddlePrefab.NewLevelPuddle(_newLfePuddleAdded,_maxDamagePuddle,_minDamagePuddle);
    }
}
