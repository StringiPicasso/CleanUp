using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Rating : MonoBehaviour
{
    [SerializeField] private TMP_Text _ratingText;

    public void RatingTotalExperience(int player,string Name)
    {
       _ratingText.text =player.ToString()+ " pts "+Name;
    }
}
