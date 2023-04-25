using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRatingPlayer : MonoBehaviour
{
    [SerializeField] private Rating _ratingText;

    private LiderboardPlayers liderboardPlayers;


    private void Awake()
    {
        liderboardPlayers = GetComponent<LiderboardPlayers>();
    }

    private void OnEnable()
    {
       // liderboardPlayers.TopChanged += OnTopChanged;
    }

    private void OnDisable()
    {
       // liderboardPlayers.TopChanged -= OnTopChanged;
    }

    public void OnTopChanged(VacuumCleaner value)
    {
       // _ratingText.RatingTotalExperience(value);
    }
}
