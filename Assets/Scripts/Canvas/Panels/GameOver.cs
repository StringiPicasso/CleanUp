using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Agava.YandexGames;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GlobalLiderboard _globalLiderboard;
    [SerializeField] private GameOver _gameOver;
    [SerializeField] private TMP_Text _youEatText;
    [SerializeField] private TMP_Text _youBrokeText;

    public event UnityAction GameOverReady;
    public event UnityAction ButtonWasPressedInGameOver;

    private void Start()
    {
        GameOverReady?.Invoke();
        Time.timeScale = 0;
    }

    public void OnEatSceenOff()
    {
        _youEatText.gameObject.SetActive(false);
    }

    public void OnBrokeSceenOn()
    {
        _youBrokeText.gameObject.SetActive(true);
    }

    public void OnRequestPersonalProfileDataPermissionButtonClick()
    {
        PlayerAccount.RequestPersonalProfileDataPermission();
    }

    public void ShowGlobalLEaderoardButton()
    {
        ButtonWasPressedInGameOver?.Invoke();
        ShowGlobalLeaderboard();
    }

    private void ShowGlobalLeaderboard()
    {
        _gameOver.gameObject.SetActive(false);
        _globalLiderboard.gameObject.SetActive(true);
    }
}

