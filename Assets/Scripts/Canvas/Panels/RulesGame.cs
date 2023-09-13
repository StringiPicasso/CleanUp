using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesGame : MonoBehaviour
{
    [SerializeField] private RulesGame _rulesFrame;

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void CloseRulesClick()
    {
        CloseRulesGame();
    }
    
    private void CloseRulesGame()
    {
        _rulesFrame.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
