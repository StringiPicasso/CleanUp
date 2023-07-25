using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private GameObject _ratingPlace;
    [SerializeField] private RatingView _templateRating;
    [SerializeField] private List<RatingView> _ratingVews;
    [SerializeField] private EnemySpawn _enemySpawn;

    public List<VacuumCleaner> _playersResultLeaderboard;
    private int _top3CountRaiting = 3;

    private void OnEnable()
    {
        _enemySpawn.SomethimgHapped += OnEnemySpawned;
        _enemySpawn.ChangeEXP += SortLeaderboard;
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

    private void SortLeaderboard()
    {
        _playersResultLeaderboard.Sort((p1, p2) => p2.TotalNumberPointExpirience.CompareTo(p1.TotalNumberPointExpirience));

        for (int i = 0; i < _top3CountRaiting; i++)
        {
            _ratingVews[i].Render(_playersResultLeaderboard[i], i + 1);
        }
    }
}
