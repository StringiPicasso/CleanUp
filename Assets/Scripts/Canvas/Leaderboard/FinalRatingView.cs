using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalRatingView : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerPositionText;
    [SerializeField] private TMP_Text _playerNameText;
    [SerializeField] private TMP_Text _playerPointText;

    public void RenderFinalView(VacuumCleaner player, int position)
    {
        _playerPositionText.text = position.ToString();
        _playerNameText.text = player.NamePlayer;
        _playerPointText.text = player.TotalNumberPointExpirience.ToString() + " pts ";
    }
}
