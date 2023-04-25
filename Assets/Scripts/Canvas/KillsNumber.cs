using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillsNumber : MonoBehaviour
{
    [SerializeField] private TMP_Text _killsCountText;

      public void TotalKillsCount(int killsCount)
    {
        _killsCountText.text = "Kills: "+killsCount.ToString();
    }
}
