using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Agava.YandexGames;

public class AdvManagement : MonoBehaviour
{
    // [SerializeField] private int _rewardVideoWheels;
    [SerializeField] private GameManagerCanvas _currentNecessaryCountWheels;

    private int _newNecessaryCountWheels;

    public event UnityAction<int> WheelsChanged;

    private void Start()
    {
        _newNecessaryCountWheels = _currentNecessaryCountWheels.NecessaryCountWheel;
    }

    public void OpenRewardAd()
    {
        VideoAd.Show(OpedAd, Reward, CloseRewardAd);
    }

    public void OnShowAd()
    {
        VideoAd.Show(OpedAd, null, CloseAd);
    }

    private void Reward()
    {
        _newNecessaryCountWheels++;
        WheelsChanged?.Invoke(_newNecessaryCountWheels);
    }

    private void OpedAd()
    {
        Time.timeScale = 0;
        AudioListener.volume = 0;
    }

    private void CloseAd()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }

    private void CloseRewardAd()
    {
        Time.timeScale = 0;
        AudioListener.volume = 1;
    }

}
