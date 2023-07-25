using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleSpawn : MonoBehaviour
{
    [SerializeField] private Pet _petPrefab;
    [SerializeField] private Puddle _puddlePrefab;
    [SerializeField] private float _minTimeSpawnPuddle;
    [SerializeField] private float _newLfePuddleAdded;

    private float _currentTimeSpawn;

    void Start()
    {
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
        var currentPuddle=Instantiate(_puddlePrefab, _petPrefab.transform.position, Quaternion.identity);
        currentPuddle.NewLifePuddle(_newLfePuddleAdded);
        _currentTimeSpawn =Random.Range( _minTimeSpawnPuddle,_petPrefab.TimeToSpawnPuddle);
    }
}
