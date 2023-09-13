using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseGlobalLiderboard : MonoBehaviour
{
    [SerializeField] private GameOver _gameOver;
    [SerializeField] private GameComplete _gameComplete;
    [SerializeField] private GlobalLiderboard _globalLiderboard;
    [SerializeField] private Button _openGameOver;
    [SerializeField] private Button _openGameComplete;

    private void OnEnable()
    {
        _gameOver.ButtonWasPressedInGameOver += OnButtonWasPressedInGameOver;
        _gameComplete.ButtonWasPressedInGameComplete += OnButtonWasPressedInGameComplete;
    }

    private void OnDisable()
    {
        _gameOver.ButtonWasPressedInGameOver -= OnButtonWasPressedInGameOver;
        _gameComplete.ButtonWasPressedInGameComplete -= OnButtonWasPressedInGameComplete;
    }

    private void OnButtonWasPressedInGameOver()
    {
        _openGameComplete.gameObject.SetActive(false);
        _openGameOver.gameObject.SetActive(true);
    }

    private void OnButtonWasPressedInGameComplete()
    {
        _openGameOver.gameObject.SetActive(false);
        _openGameComplete.gameObject.SetActive(true);
    }

    private void CloseGlobalLoderboardToGameOver()
    {
        _globalLiderboard.gameObject.SetActive(false);
        _gameOver.gameObject.SetActive(true);
    }

    private void CloseGlobalLoderboardToGameComplete()
    {
        _globalLiderboard.gameObject.SetActive(false);
        _gameComplete.gameObject.SetActive(true);
    }

    public void CloseGlobalLoderboardGameOverBottom()
    {
        CloseGlobalLoderboardToGameOver();
    }

    public void CloseGlobalLoderboardGameCompleteBottom()
    {
        CloseGlobalLoderboardToGameComplete();
    }
}
