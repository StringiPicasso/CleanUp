using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnPet : MonoBehaviour
{
    [SerializeField] private Transform[] _pointsSpawnCat;
    [SerializeField] private TimeToSpawnOrdinaryCat _timeSpawnOrdinaryCat;
    [SerializeField] private Pet _petPrefab;
    [SerializeField] private InstantiateSpawnTextCanvas _gearWheelSpawnText;
    [SerializeField] private Puddle _puddlePrefab;
    [SerializeField] private int _maxDamagePuddle;
    [SerializeField] private int _minDamagePuddle;

    private int _randomSpawnPointIndex;

    public event UnityAction<Pet> PetSpawned;

    private void OnEnable()
    {
        _timeSpawnOrdinaryCat.TimeSpawnBlackCatCame += OnCatStrongerSpawed;
        _timeSpawnOrdinaryCat.TimeSpawnPetCame += OnSpawnOrdinaryPet;
    }

    private void OnDisable()
    {
        _timeSpawnOrdinaryCat.TimeSpawnPetCame -= OnSpawnOrdinaryPet;
    }

    private void Start()
    {
        _puddlePrefab.FirstSpawnFromCat(_minDamagePuddle, _maxDamagePuddle);
    }

    private void OnSpawnOrdinaryPet()
    {
        var currentPet = SpawnNewCat();
       // currentPet.NewLevelCat(_newLifeCat);
        NotificationForNewCat(currentPet, "В дом зашел кот!");
    }

    private void OnCatStrongerSpawed()
    {
        var currentPet = SpawnNewCat();
        currentPet.UpgradeStrongCat();
    }

    private void NotificationForNewCat(Pet currentPet, string textNotification)
    {
        PetSpawned?.Invoke(currentPet);
        _gearWheelSpawnText.gameObject.SetActive(true);
        _gearWheelSpawnText.CreateTextOnDisplay(textNotification);
    }

    private Pet SpawnNewCat()
    {
        _randomSpawnPointIndex = Random.Range(0, _pointsSpawnCat.Length);
        var currentPet = Instantiate(_petPrefab, _pointsSpawnCat[_randomSpawnPointIndex]);

        return currentPet;
    }
}
