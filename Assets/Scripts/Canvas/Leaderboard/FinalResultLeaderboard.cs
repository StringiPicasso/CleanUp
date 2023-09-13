using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalResultLeaderboard : MonoBehaviour
{
    [SerializeField] private RatingLive _resultsPlayers;
    [SerializeField] private FinalRatingView _templateFinalRating;
    [SerializeField] private FinalRatingView _templateBadrating;
    [SerializeField] private GameObject _ratingPlace;
    [SerializeField] private Player _player;

    public List<VacuumCleaner> _finalGameLeaderboard = new List<VacuumCleaner>();

    private bool IsPlayerOffRating;

    private void Awake()
    {
        IsPlayerOffRating = false;

        for (int i = 0; i < _resultsPlayers._playersResultLeaderboard.Count; i++)
        {
            if (_resultsPlayers._playersResultLeaderboard[i].TotalExperienceForLiderboard > 0)
            {
                _finalGameLeaderboard.Add(_resultsPlayers._playersResultLeaderboard[i]);
            }
        }
    }

    private void OnEnable()
    {
        _finalGameLeaderboard.Sort((p1, p2) => p2.TotalExperienceForLiderboard.CompareTo(p1.TotalExperienceForLiderboard));

        for (int i = 0; i < _finalGameLeaderboard.Count; i++)
        {
            var view = Instantiate(_templateFinalRating, _ratingPlace.transform);
            view.RenderFinalView(_finalGameLeaderboard[i], (i + 1).ToString());
        }

        ShowPlayerPlace();
    }

    private void ShowPlayerPlace()
    {
        foreach (var name in _finalGameLeaderboard)
        {
            if (name.NamePlayer == _player.NamePlayer)
            {
                IsPlayerOffRating = true;

                return;
            }
        }

        if (!IsPlayerOffRating)
        {
            var view = Instantiate(_templateBadrating, _ratingPlace.transform);
            view.RenderFinalView(_player, ":c");
        }
    }
}
