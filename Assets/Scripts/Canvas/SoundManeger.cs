using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManeger : MonoBehaviour
{
   [SerializeField] private AudioSource _audioSource;
   [SerializeField] private AudioClip _clickSound;
   [SerializeField] private AudioClip _gameOverSound;
   [SerializeField] private AudioClip _gameCompleteSound;
   [SerializeField] private AudioClip _notificationSound;
    [SerializeField] private GameOver _gameOverPrefab;
    [SerializeField] private GameComplete _gameCompletePrefab;
    [SerializeField] private InstantiateSpawnTextCanvas _textEnabled;

    private void Start()
    {
        _textEnabled.NotificationEnabled += OnNotificationEnabled;
        _gameCompletePrefab.GameCompleteReady += OnGameCompleteReady;
        _gameOverPrefab.GameOverReady += OnGameOverReady;
    }

    private void OnDisable()
    {
        _textEnabled.NotificationEnabled -= OnNotificationEnabled;
        _gameCompletePrefab.GameCompleteReady -= OnGameCompleteReady;
        _gameOverPrefab.GameOverReady -= OnGameOverReady;
    }

    public void ClickSoundButton()
    {
        _audioSource.PlayOneShot(_clickSound);
    }

    public void OnNotificationEnabled()
    {
        _audioSource.PlayOneShot(_notificationSound);
    }

    public void OnGameOverReady()
    {
        _audioSource.PlayOneShot(_gameOverSound);
    }

    public void OnGameCompleteReady()
    {
        _audioSource.PlayOneShot(_gameCompleteSound);
    }
}
