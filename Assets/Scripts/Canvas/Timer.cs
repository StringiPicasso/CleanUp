using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _timeGame;
    [SerializeField] private TMP_Text _timeDisplay;
    [SerializeField] private GameComplete _imGameComplete;
    [SerializeField] private float _startTimeSpawnEnemy;
    [SerializeField] private float _timeAddedNextTimeSpawnEnemy;
    [SerializeField] private float _firstTimeSpawnEnemy;
    [SerializeField] private float _firstSpawnCatTime;

    private float _startTimefirstSpawnCat;
    private float _currentTimeCatToSpawn;
    private float _currentTimeEnemy;
    public event UnityAction TimeSpawnEnemyCame;
    public event UnityAction TimeSpawnPetCame;

    private void Start()
    {
        _currentTimeEnemy = _firstTimeSpawnEnemy;
        _startTimeSpawnEnemy = _firstTimeSpawnEnemy;
        _startTimefirstSpawnCat = _firstSpawnCatTime;
        _currentTimeCatToSpawn = _firstSpawnCatTime;
    }

    private void Update()
    {
        if (_timeGame <= 0)
        {
            _imGameComplete.gameObject.SetActive(true);
        }
        else
        {
            _timeGame -= Time.deltaTime;
            _currentTimeEnemy -= Time.deltaTime;
            _currentTimeCatToSpawn -= Time.deltaTime;
            System.TimeSpan time = System.TimeSpan.FromSeconds(_timeGame);
            _timeDisplay.text = time.Minutes.ToString("00") + ":" + time.Seconds.ToString("00");

            if (_currentTimeEnemy <= 0)
            {
                TimeSpawnEnemyCame?.Invoke();
                _firstTimeSpawnEnemy -= _timeAddedNextTimeSpawnEnemy;
                _currentTimeEnemy = _firstTimeSpawnEnemy;

                if (_firstTimeSpawnEnemy <= 0)
                {
                    _firstTimeSpawnEnemy = _startTimeSpawnEnemy - _timeAddedNextTimeSpawnEnemy;
                }
            }

            if (_currentTimeCatToSpawn <= 0)
            {
                TimeSpawnPetCame?.Invoke();
                _firstSpawnCatTime -= _timeAddedNextTimeSpawnEnemy;
                _currentTimeCatToSpawn = _firstSpawnCatTime;

                if (_firstSpawnCatTime <= 0)
                {
                    _firstSpawnCatTime = _startTimefirstSpawnCat - _timeAddedNextTimeSpawnEnemy;
                }
            }
        }
    }
}
