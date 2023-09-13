using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartGameSceen : MonoBehaviour
{
    [SerializeField] private StartGameSceen _startGameScreenPrefab;
    [SerializeField] private GameManagerCanvas _playerColor;
    [SerializeField] private RulesGame _openRulesPrefab;

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void StartGameBottom()
    {
        StartTheGame();
    }

    public void ChangeColorClick(Button item)
    {
        _playerColor.SaveColorPlayer(item.image.color);
    }
  
    private void StartTheGame()
    {
        _startGameScreenPrefab.gameObject.SetActive(false);
        _openRulesPrefab.gameObject.SetActive(true);
    }
}
