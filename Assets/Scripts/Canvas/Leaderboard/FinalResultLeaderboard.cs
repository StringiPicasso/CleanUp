using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalResultLeaderboard : MonoBehaviour
{
    [SerializeField] private Leaderboard _resultsPlayers;
    [SerializeField] private FinalRatingView _templateFinalRating;
    [SerializeField] private GameObject _ratingPlace;

    private void Awake()
    {
        _resultsPlayers._playersResultLeaderboard.Sort((p1, p2) => p2.TotalNumberPointExpirience.CompareTo(p1.TotalNumberPointExpirience));

        for (int i = 0; i < _resultsPlayers._playersResultLeaderboard.Count; i++)
        {
            var view = Instantiate(_templateFinalRating, _ratingPlace.transform);
            view.RenderFinalView(_resultsPlayers._playersResultLeaderboard[i], i + 1);
        }
    }
}
