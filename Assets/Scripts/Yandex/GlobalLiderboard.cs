using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;

public class GlobalLiderboard : MonoBehaviour
{
    [SerializeField] private LeaderboardViewRang _templateOrdinaryView;
    [SerializeField] private LeaderboardViewRang _templateFirstView;
    [SerializeField] private LeaderboardViewRang _templateSecondView;
    [SerializeField] private LeaderboardViewRang _templateThirdView;
    [SerializeField] private GameObject _viewPlace;
    [SerializeField] private GameManagerCanvas gameManagerCanvas;
    [SerializeField] private List<LeaderboardViewRang> _ratingVews=new();

    private readonly string _leadearboardName = "GlobalLeaderboard";
    private readonly string _anonymousName = "Anonymous";

    private void OnEnable()
    {
        Time.timeScale = 0;
        _ratingVews.Clear();

        PlayerAccount.Authorize();

        if (PlayerAccount.IsAuthorized == false)
        {
            PlayerAccount.Authorize();
        }

        if (PlayerAccount.IsAuthorized == true)
        {
            Leaderboard.SetScore(_leadearboardName, gameManagerCanvas.GlobalPointPlayer);
                ShowAllUsers();
          /*  Leaderboard.GetPlayerEntry(_leadearboardName, (result) =>
            {
            /*    if (result == null)
                {
           // ShowAllUsers();
                }
                else
                {
                    //Leaderboard.SetScore(_leadearboardName, result.score);
                   // ShowAllUsers();
                }
            });*/
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void ShowAllUsers()
    {
      //  _ratingVews.Clear();

        Leaderboard.GetEntries(_leadearboardName, (result) =>
        {
            for (int i = 0; i < result.entries.Length; i++)
            {
                if (i == 0)
                {
                    CreateViewrang(i,result.entries[i], _templateFirstView);
                }
                if (i == 1)
                {
                    CreateViewrang(i,result.entries[i], _templateSecondView);
                }
                if (i == 2)
                {
                    CreateViewrang(i,result.entries[i], _templateThirdView);
                }
                if (i >=3)
                {
                    CreateViewrang(i,result.entries[i], _templateOrdinaryView);
                }
            }
        });
    }

    private void CreateViewrang(int i,LeaderboardEntryResponse entry,LeaderboardViewRang viewRating)
    {

        if (entry.score > 0)
        {
        string name;
            var view = Instantiate(viewRating, _viewPlace.transform);
            name = entry.player.publicName;

            if (string.IsNullOrEmpty(name))
                name = _anonymousName;
           
            _ratingVews.Add(view);

        _ratingVews[i].RenderView(entry.rank, name, entry.score);
          //  _ratingVews..RenderView(entry.rank, name, entry.score);
        }
        
    }
}
