using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Agava.YandexGames;

public class GameComplete : MonoBehaviour
{
    [SerializeField] private GlobalLiderboard _globalLiderboard;
    [SerializeField] private GameComplete _gameComplete;
    [SerializeField] private FinalResultLeaderboard _topPlayers;
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _youreNumberOne;
    [SerializeField] private TMP_Text _youreGood;

    public event UnityAction GameCompleteReady;
    public event UnityAction ButtonWasPressedInGameComplete;
 
    private void Start()
    {
        _youreGood.gameObject.SetActive(true);
        GameCompleteReady?.Invoke();
        Time.timeScale = 0;
        CheckTopPlayer();
    }

    public void OnRequestPersonalProfileDataPermissionButtonClick()
    {
        PlayerAccount.RequestPersonalProfileDataPermission();
    }

    public void ShowGlobalLEaderoardButton()
    {
        ButtonWasPressedInGameComplete?.Invoke();
        ShowGlobalLeaderboard();
    }

    private void ShowGlobalLeaderboard()
    {
        _gameComplete.gameObject.SetActive(false);
        _globalLiderboard.gameObject.SetActive(true);
    }

    private void CheckTopPlayer()
    {
        if (_player.NamePlayer == _topPlayers._finalGameLeaderboard[0].NamePlayer)
        {
            _youreGood.gameObject.SetActive(false);
            _youreNumberOne.gameObject.SetActive(true);
        }
    }
}
