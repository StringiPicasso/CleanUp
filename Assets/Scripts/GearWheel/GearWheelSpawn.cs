using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearWheelSpawn : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GearWheel _gearWheelPrefab;
    [SerializeField] private InstantiateSpawnTextCanvas _gearWheelSpawnText;
    [SerializeField] private GameManagerCanvas _counWheels;
    [SerializeField] private LocationSize _areaSpawn;
    [SerializeField] private int _necessaryEXPCountForSpawnWheel;
    [SerializeField] private int _addedNextEXPCount;

    private Vector3 _randomSpawnPosition;


    private void OnEnable()
    {
        _player.TimeToSpawnWheelCame += OnTimeToSpawnWheelCame;
    }

    private void OnDisable()
    {
        _player.TimeToSpawnWheelCame -= OnTimeToSpawnWheelCame;
    }

    private void OnTimeToSpawnWheelCame()
    {
        if (_player.TotalExperienceForLiderboard >= _necessaryEXPCountForSpawnWheel && _player.CountGearWheel < _counWheels.NecessaryCountWheel)
        {
            _randomSpawnPosition = _areaSpawn.ChooseRandomSpawnPozitionXZ(_gearWheelPrefab.transform.position.y);

            var wheel=Instantiate(_gearWheelPrefab, _randomSpawnPosition, Quaternion.identity);
            _necessaryEXPCountForSpawnWheel += _addedNextEXPCount;

            _gearWheelSpawnText.CreateTextOnDisplay("Появилась шестеренка");
        }
    }
}
