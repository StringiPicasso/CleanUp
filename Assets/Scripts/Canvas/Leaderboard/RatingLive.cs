using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatingLive : MonoBehaviour
{
    [SerializeField] private GameObject _ratingPlace;
    [SerializeField] private RatingLiveView _templateRating;
    [SerializeField] private List<RatingLiveView> _ratingVews;
    [SerializeField] private EnemySpawn _enemySpawn;

    private List<VacuumCleaner> _finalLeader;
    public List<VacuumCleaner> _playersResultLeaderboard;

    private int _top3CountRaiting = 3;
    
    private void OnEnable()
    {
        _enemySpawn.SomethimgHapped += OnEnemySpawned;
        _enemySpawn.ChangeEXP += ChangePoint;
    }

    private void Start()
    {
        for (int i = 0; i < _top3CountRaiting; i++)
        {
            AddPlayers();
        }
    }

    private void AddPlayers()
    {
        var view = Instantiate(_templateRating, _ratingPlace.transform);
        _ratingVews.Add(view);
    }

    private void OnEnemySpawned(List<VacuumCleaner> vacuumCleaners)
    {
        _playersResultLeaderboard = vacuumCleaners;
        SortLeaderboard();
    }

    private void ChangePoint(string name)
    {
        SortLeaderboard();

        for (int i = 0; i < _top3CountRaiting; i++)
        {
            if (_ratingVews[i].NameRating == name)
            {
                _ratingVews[i].AnimateText();
            }
        }
    }

    private void SortLeaderboard()
    {
        _playersResultLeaderboard.Sort((p1, p2) => p2.TotalExperienceForLiderboard.CompareTo(p1.TotalExperienceForLiderboard));

        for (int i = 0; i < _top3CountRaiting; i++)
        {
            _ratingVews[i].RenderLiveView(_playersResultLeaderboard[i], (i + 1).ToString());
           
        }
    }
}
