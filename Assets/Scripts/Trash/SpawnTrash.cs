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
    private Vector3 _originalPOint;
    private float _currentTimeSpawn;
    private int _randomIndex;
    private bool isPlaceFree;

    private void Start()
    {
        _originalPOint = transform.position;
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

    private bool CheckFreeePOsition(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapBox(position, _originalPOint);

        if (hitColliders.Length > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void InstantiateTrash()
    {
        _randomIndex = Random.Range(0, _trash.Length);
        _randomSpawnPosition = new Vector3(Random.Range(-20, 20), _trash[_randomIndex].transform.position.y, Random.Range(-20, 30));

        isPlaceFree = CheckFreeePOsition(_randomSpawnPosition);

        if (isPlaceFree)
        {
            Instantiate(_trash[_randomIndex], _randomSpawnPosition, Quaternion.identity);
        }
        else
        {
            InstantiateTrash();
        }
    }
}
