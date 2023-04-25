using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnTrash : MonoBehaviour
{
    [SerializeField] private Trash[] _trash;
    [SerializeField] private float _timeAfterLastSpawn;
    [SerializeField] private int _miminumRandomNumber;
    [SerializeField] private int _maximumRandomNumber;

    private Vector3 _randomSpawnPosition;
    private float _currentTimeSpawn;
    private int _randomIndex;
    private Trash _target;

    public Trash TargetTrash => _target;

    public event UnityAction<Trash> TrashCreated;

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
        _randomSpawnPosition = new Vector3(Random.Range(-_miminumRandomNumber, _maximumRandomNumber), _trash[_randomIndex].transform.position.y, Random.Range(-_miminumRandomNumber, _maximumRandomNumber));

        _target=Instantiate(_trash[_randomIndex], _randomSpawnPosition, Quaternion.identity);
        TrashCreated?.Invoke(_target);
    }
}
