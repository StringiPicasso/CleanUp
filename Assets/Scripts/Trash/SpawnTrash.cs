using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnTrash : MonoBehaviour
{
    [SerializeField] private Trash[] _trash;
    [SerializeField] private LocationSize _areaSpawn;
    [SerializeField] private float _timeAfterLastSpawn;

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

    private void InstantiateTrash()
    {
        _randomIndex = Random.Range(0, _trash.Length);
        _randomSpawnPosition=_areaSpawn.ChooseRandomSpawnPozitionXZ(_trash[_randomIndex].transform.position.y);
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

}
