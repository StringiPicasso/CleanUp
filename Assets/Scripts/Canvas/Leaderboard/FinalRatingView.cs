using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalRatingView : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerPositionText;
    [SerializeField] private TMP_Text _playerNameText;
    [SerializeField] private TMP_Text _playerPointText;

    public void RenderFinalView(VacuumCleaner player, string position)
    {
        _playerPositionText.text = position;
        _playerNameText.text = player.NamePlayer;
        _playerPointText.text = player.TotalExperienceForLiderboard.ToString();
    }
}
