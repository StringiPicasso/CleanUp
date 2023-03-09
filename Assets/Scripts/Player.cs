using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxExperience;
    [SerializeField] private int _minExperience;
    [SerializeField] private ExperiencePointsText _notificationNewReward;

    private Animator _animator;
    private Vector3 _newLocalScale=new Vector3(1,1,1);

    private int _experienceCount;
    private float _maxPercent = 100;
    private float _currentXPPercent;
    private float _currentImageCirclePercent;
    private int _summandNewExperiencePoints=20;

    public event UnityAction<float> ExperienceChanged;

    public float CurrentXPImagineCirclePercent => _currentImageCirclePercent;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _newLocalScale = transform.localScale;
    }

    public void ChangeExperienceCount(int reward)
    {
        CalculateCurrentPercent(reward);
        _animator.Play("PickUpTrash");
        ExperienceChanged?.Invoke(CurrentXPImagineCirclePercent);
    }

    private void CalculateCurrentPercent(int reward)
    {
        _experienceCount += reward;
        NotificationReceivePOints(reward);
        _currentXPPercent = (_experienceCount * _maxPercent) / _maxExperience;
        _currentImageCirclePercent = _currentXPPercent/_maxPercent;
    }

    private void NotificationNewReward(int reward)
    {
        _notificationNewReward.gameObject.SetActive(true);
        _notificationNewReward.NotificationNewReward(reward);
    }

    private void TakeNewLevel()
    {
        _notificationNewReward.gameObject.SetActive(true);
        _notificationNewReward.NotificationNextLevel();
    }

    private void NewLocalScale()
    {
        
        transform.localScale += _newLocalScale;

    }

    private void NotificationReceivePOints(int reward)
    {
        NotificationNewReward(reward);
        
        if (_experienceCount >= _maxExperience)
        {
            TakeNewLevel();
            _experienceCount = _minExperience;
            _maxExperience += _summandNewExperiencePoints;
        }
    }
}
