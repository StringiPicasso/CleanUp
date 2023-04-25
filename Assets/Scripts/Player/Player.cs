using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(Animator))]

public class Player : VacuumCleaner
{
    [SerializeField] private KillsNumber _killsCountText;

    // private ExperiencePointsText _currentText;

    private float _maxPercent = 100;
    private float _currentXPPercent;
    private float _currentImageCirclePercent;
    private int _totalKills = 0;

    public event UnityAction<float> ExperienceChanged;

    public float CurrentXPImagineCirclePercent => _currentImageCirclePercent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Trash>(out Trash trash))
        {
            if (trash.LevelTrash < _level)
            {
                ChangeExerience(trash.RewardTrash);
                Destroy(trash.gameObject);
            }
        }
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (enemy.Level < _level)
            {
                _totalKills++;
                ChangeExerience(enemy.Reward);
                Destroy(enemy.gameObject);
            }
        }
    }

    public override void ChangeExerience(int reward)
    {
        TryGetReward(reward);
        ChangeExperienceCount(reward);
    }

    private void ChangeExperienceCount(int reward)
    {
        _killsCountText.TotalKillsCount(_totalKills);
        CalculateCurrentPercent(reward);
        ExperienceChanged?.Invoke(CurrentXPImagineCirclePercent);
    }

    private void CalculateCurrentPercent(int reward)
    { 
        NotificationReceivePOints(reward);
        _currentXPPercent = (_totalExperience * _maxPercent) / _maxExperience;
        _currentImageCirclePercent = _currentXPPercent/_maxPercent;
    }

    private void NotificationNewReward(int reward)
    {
      _notificationNewReward.gameObject.SetActive(true);
        _notificationNewReward.NotificationNewReward(reward);
    }

    private void NotificationReceivePOints(int reward)
    {
        NotificationNewReward(reward);
    }
}
