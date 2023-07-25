using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRules : MonoBehaviour
{
    [SerializeField] private GameObject _rulesFrame;

    private void CloseRulesGame()
    {
        _rulesFrame.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void CloseRulesButton()
    {
        CloseRulesGame();
    }
}
