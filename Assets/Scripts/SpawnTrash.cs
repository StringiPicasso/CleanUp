using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrash : MonoBehaviour
{
    [SerializeField] private Trash[] _trash;
    [SerializeField] private float _timeAfterLastSpawn;
    [SerializeField] private int _miminumRandomNumber;
    [SerializeField] private int _maximumRandomNumber;

    private Vector3 _randomSpawnPosition;
    private float _currentTimeSpawn;
    private int _randomIndex;

    private void Start()
    {
        _currentTimeSpawn = _timeAfterLastSpawn;
    }

    private void Update()
    {
        if (_currentTimeSpawn <= 0)
        {
            InstantiateTrash();
            _currentTimeSpawn = _timeAfterLastSpawn;
        }

        _currentTimeSpawn -= Time.deltaTime;
    }

    private void InstantiateTrash()
    {
        _randomIndex = Random.Range(0, _trash.Length);
        _randomSpawnPosition = new Vector3(Random.Range(-_miminumRandomNumber, _maximumRandomNumber), 0, Random.Range(-_miminumRandomNumber, _maximumRandomNumber));

        Instantiate(_trash[_randomIndex], _randomSpawnPosition, Quaternion.identity);
    }
}
