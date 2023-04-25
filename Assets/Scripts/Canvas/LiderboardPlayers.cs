using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class LiderboardPlayers : MonoBehaviour
{
    [SerializeField] private List<VacuumCleaner> _players;
    [SerializeField] private List<VacuumCleaner> _top3Players;
   // [SerializeField] private UIRatingPlayer _UIRatingPlayer;
    [SerializeField] private EnemySpawn _enemies;
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;

   //[SerializeField] Rating Rating;
   [SerializeField] List<Rating> _ratingList;

    private VacuumCleaner _playersData;
    private int _countPlayersOfTop = 3;

   // public event UnityAction<VacuumCleaner> TopChanged;

    private void Awake()
    {
        _playersData = GetComponent<VacuumCleaner>();
    }

    private void OnEnable()
    {
       // _player.ExperienceChanged += SortPlayers;
        _enemies.EnemyCreated += OnEnemyCreate;
        _enemy.EnemyDying += OnEnemyDied;
    }

    private void Start()
    {
        _players.Add(_player);
    }

    private void OnDisable()
    {
       // _player.ExperienceChanged -= SortPlayers;


        _enemies.EnemyCreated -= OnEnemyCreate;
        _enemy.EnemyDying -= OnEnemyDied;
    }

    private void OnEnemyCreate(Enemy enemy)
    {
        _players.Add(enemy);
    }

    private void OnEnemyDied(Enemy enemy)
    {
        _players.Remove(enemy);
    }

    private void Update()
    {
       // CreateRang();
      /*  foreach (var item in _players)
        {
            Rating.RatingTotalExperience(item.TotalNumberPointExpirience,item.Name);
        }*/
    }

    private void SortTopPlayers()
    {
        for (int i = 0; i < _countPlayersOfTop; i--)
        {
            _ratingList[i].RatingTotalExperience(_top3Players[i].TotalNumberPointExpirience, _top3Players[i].Name);
        }
        
    }

    private void CreateRang()
    {
        var filterPlayers = _players.OrderByDescending(players => players.TotalNumberPointExpirience).Take(_countPlayersOfTop);
      //  _players.Sort((p1, p2) => p2.TotalNumberPointExpirience.CompareTo(p1.TotalNumberPointExpirience));

       /* foreach (var p in _players)
            print(p.name + " " + p.TotalNumberPointExpirience);*/

        foreach (var item in filterPlayers)
        {
            _top3Players.Add(item);
                     //   Rating.RatingTotalExperience(item.TotalNumberPointExpirience, item.Name);
        }

        SortTopPlayers();
    }

    /*  private void SortPlayers(float cleaner)
      {
          Debug.Log(cleaner);
         //TopChanged?.Invoke(cleaner);
         // _Ratings.Add(_UIRatingPlayer);
         /*var filterPlayers = _players.OrderByDescending(players => players.TotalNumberPointExpirience).Take(_countPlayersOfTop);

          foreach (var player in filterPlayers)
          {
              _UIRatingPlayer=_UIRatingPlayer.OnTopChanged(cleaner);
              _Ratings.Add(_UIRatingPlayer.OnTopChanged(cleaner);
             // TopChanged?.Invoke(player);
             // Instantiate(_UIRatingPlayer);
          }
          //var sortedPlayers = _players.OrderBy(p => p.TotalNumberPointExpirience);
          // var sortedPlayers = from p in _players orderby p.Name select p;*/
}

