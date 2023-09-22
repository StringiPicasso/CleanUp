using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameNewRestart : MonoBehaviour
{
    [SerializeField] private MenuNewGame _menuOpen;
    [SerializeField] private GameComplete _gameCompleteClose;

    void Start()
    {
        Time.timeScale = 0;
    }

    public void NewGameMenuButton()
    {
        NewGameMenu();
    }

    private void NewGameMenu()
    {
        _gameCompleteClose.gameObject.SetActive(false);
        _menuOpen.gameObject.SetActive(true);
    }
}
