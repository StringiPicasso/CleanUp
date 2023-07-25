using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameComplete : MonoBehaviour
{
    [SerializeField] private Leaderboard _topPleyers;
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _youreNumberOne;

    private void Start()
    {
        Time.timeScale = 0;
        CheckTopPlayer();
    }

    private void CheckTopPlayer()
    {
        if (_player.NamePlayer == _topPleyers._playersResultLeaderboard[0].NamePlayer)
        {
            _youreNumberOne.gameObject.SetActive(true);
        }
    }
}
