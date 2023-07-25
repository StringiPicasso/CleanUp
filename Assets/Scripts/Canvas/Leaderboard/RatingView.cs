using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RatingView : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerText;

    public void Render(VacuumCleaner player, int position)
    {
        _playerText.text =position.ToString() +" - "+ player.TotalNumberPointExpirience.ToString() + " pts " + player.NamePlayer;
    }
}
