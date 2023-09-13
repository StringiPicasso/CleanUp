using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSize : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnArea;
    [SerializeField] private float _adjustingBoundsSize = 2.3f;

    private int _randomAreaIndex;
    private float _areaSizeX;
    private float _areaSizeZ;
    private float _areaSizeY;

    private Vector3 _areaCenter;

    public Vector3 ChooseRandomSpawnPozitionXZ(float positionY)
    {
          if (_spawnArea.Count == 1)
          {
              _randomAreaIndex = 0;
          }
          else
          {
              _randomAreaIndex = Random.Range(0, _spawnArea.Count);
          }
        
        _areaCenter = _spawnArea[_randomAreaIndex].position;
        _areaSizeX = _spawnArea[_randomAreaIndex].GetComponent<MeshRenderer>().bounds.size.x / _adjustingBoundsSize;
        _areaSizeZ = _spawnArea[_randomAreaIndex].GetComponent<MeshRenderer>().bounds.size.z / _adjustingBoundsSize;
        _areaSizeY = positionY;

        return new Vector3(Random.Range(_areaCenter.x-_areaSizeX, _areaCenter.x+_areaSizeX), _areaSizeY, Random.Range(_areaCenter.z-_areaSizeZ, _areaCenter.z+_areaSizeZ));
    }


}
