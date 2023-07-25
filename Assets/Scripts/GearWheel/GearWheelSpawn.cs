using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearWheelSpawn : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GearWheel _gearWheelPrefab;
    [SerializeField] private SpawnGearWheelText _gearWheelSpawnText;

    private Vector3 _randomSpawnPosition;
    private int _necessaryEXPCountForSpawnWheel = 15;
    private int _addedNextEXPCount = 15;

    private void OnEnable()
    {
        _player.ExperienceTakedForCheckBonus += OnExperienceTakedForCheckBonus;
    }

    private void OnDisable()
    {
        _player.ExperienceTakedForCheckBonus -= OnExperienceTakedForCheckBonus;
    }

    void OnExperienceTakedForCheckBonus()
    {
        if (_player.TotalNumberPointExpirience >= _necessaryEXPCountForSpawnWheel && _player.CountGearWheel < _gearWheelPrefab.NecessaryCountWheel)
        {
            _randomSpawnPosition = new Vector3(Random.Range(-20, 20), _gearWheelPrefab.transform.position.y, Random.Range(-20, 30));
            Instantiate(_gearWheelPrefab, _randomSpawnPosition, Quaternion.identity);
            _necessaryEXPCountForSpawnWheel += _addedNextEXPCount;

            _gearWheelSpawnText.gameObject.SetActive(true);
            _gearWheelSpawnText.GearWheelTextSpawn("Появилась шестеренка");
        }
    }
}
