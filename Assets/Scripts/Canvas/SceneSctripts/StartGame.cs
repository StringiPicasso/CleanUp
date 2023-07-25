using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private StartGameSceen _startGameScreenPrefab;
    [SerializeField] private GameObject _closeRulesPrefab;

    private void StartTheGame()
    {
        _startGameScreenPrefab.gameObject.SetActive(false);
        _closeRulesPrefab.gameObject.SetActive(true);
    }

    public void StartGameBottom()
    {
        StartTheGame();
    }
}
