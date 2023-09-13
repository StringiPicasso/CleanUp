using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InstantiateSpawnTextCanvas : MonoBehaviour
{
    [SerializeField] private SpawnNotification _spawntext;
    [SerializeField] private SpawnPointNotification _spawnPointNotification;
    [SerializeField] private GameObject _spawntextPlace;

    public event UnityAction NotificationEnabled;

    public void CreateTextOnDisplay(string text)
    {
        var currentText = Instantiate(_spawntext, _spawntextPlace.transform);
        NotificationEnabled?.Invoke();
        currentText.SpawnText(text);
    }

    public void CreateLevelPointNotificationOnDisplay(string text)
    {
        var currentPointNotification = Instantiate(_spawnPointNotification, _spawntextPlace.transform);
        currentPointNotification.SpawnNewNotification(text);
    }
}
