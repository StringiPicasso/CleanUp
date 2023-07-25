using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnPet : MonoBehaviour
{
    [SerializeField] private Pet _petPrefab;
    [SerializeField] private SpawnGearWheelText _gearWheelSpawnText;
    [SerializeField] private Timer _timeSpawn;
    [SerializeField] private float _newLifeCat;

    private Vector3 _randomSpawnPosition;

    public event UnityAction<Pet> PetSpawned;

    private void OnEnable()
    {
        _timeSpawn.TimeSpawnPetCame += OnSpawnPet;
    }

    private void OnDisable()
    {
        _timeSpawn.TimeSpawnPetCame -= OnSpawnPet;
    }
    private void Start()
    {
        OnSpawnPet();
    }

    void OnSpawnPet()
    {
        _randomSpawnPosition = new Vector3(Random.Range(-20, 20), _petPrefab.transform.position.y, Random.Range(-20, 30));
        var currentPet = Instantiate(_petPrefab, _randomSpawnPosition, Quaternion.identity);
        currentPet.NewLevelCat(_newLifeCat);
        PetSpawned?.Invoke(currentPet);

        _gearWheelSpawnText.gameObject.SetActive(true);
        _gearWheelSpawnText.GearWheelTextSpawn("В дом зашел кот!");
    }
}
