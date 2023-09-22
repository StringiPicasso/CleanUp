using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNewGame : MonoBehaviour
{
    [SerializeField] private GameManagerCanvas _newGameRestart;

    void Start()
    {
        Time.timeScale = 0;
    }
 
    public void NewGameButton()
    {
       _newGameRestart.NewGame();
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
}
