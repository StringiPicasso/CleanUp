using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseGlobalLiderboard : MonoBehaviour
{
    [SerializeField] private GameOver _gameOver;
    [SerializeField] private GameComplete _gameComplete;
    [SerializeField] private GlobalLiderboard _globalLiderboard;
  
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
