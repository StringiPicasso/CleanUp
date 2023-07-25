using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]

public abstract class VacuumCleaner : MonoBehaviour
{
    private const string PickUpObject = "PickUpTrash";

    [SerializeField] protected int _rewardValue;
    [SerializeField] protected int _maxExperience;
    [SerializeField] protected ExperiencePointsText _notificationNewReward;
    [SerializeField] protected TMP_Text _namePlayerText;
    [SerializeField] protected AnimationEdit _sckaleScript;

    protected int _lostPoint;
    protected Coroutine _activeCoroutinePuddle;
    protected Animator _animator;
    protected int _minExperience = 0;
    protected int _level = 1;
    protected int _totalExperience;
    protected int _receivedReward;
    protected int _numberNewMaxExperienceAdded = 20;
    protected int _totalNumberPointsExperience;

    public int Level => _level;
    public int Reward => _rewardValue;
    public string NamePlayer => _namePlayerText.text;
    public int TotalNumberPointExpirience => _totalNumberPointsExperience;

    public abstract void ChangeExerience(int reward);
    public abstract void LostExerience(int lostPoint);

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _lostPoint = 0;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Puddle>(out Puddle puddle))
        {
            if (_activeCoroutinePuddle != null)
            {
                StopCoroutine(_activeCoroutinePuddle);
            }

            _lostPoint = 0;
        }
    }

    public void TryGetReward(int reward)
    {
        _totalNumberPointsExperience += reward;
        _receivedReward = reward;
        ChangeTotalExperience(_receivedReward);
    }

    public void TryLostReward(int lostPoint)
    {
        _totalNumberPointsExperience -= lostPoint;
        _receivedReward = lostPoint;
        LostTotalEXP(_receivedReward);
    }

    private void LostTotalEXP(int lostPoint)
    {
        _totalExperience -= lostPoint;
        TakeLawLevel();
    }

    private void ChangeTotalExperience(int reward)
    {
        _totalExperience += reward;
        _animator.Play(PickUpObject);
        TakeNewLevel();
    }

    private void TakeLawLevel()
    {
        if (_totalExperience < _minExperience)
        {
            TakeLowLevelText();
            _maxExperience -= _numberNewMaxExperienceAdded;
            _level--;
            _sckaleScript.TakeLooseObjectSize();
        }
    }

    private void TakeNewLevel()
    {
        if (_totalExperience >= _maxExperience)
        {
            TakeNewLevelText();
            _sckaleScript.TakeNewObjectSize();
            _totalExperience = _minExperience;
            _maxExperience += _numberNewMaxExperienceAdded;
            _level++;
        }
    }

    private void TakeLowLevelText()
    {
        _notificationNewReward.gameObject.SetActive(true);
        _notificationNewReward.NotificationLowLevel();
    }

    private void TakeNewLevelText()
    {
        _notificationNewReward.gameObject.SetActive(true);
        _notificationNewReward.NotificationNextLevel();
    }

    protected IEnumerator RaseLostPoint()
    {
        while (true)
        {
            _lostPoint++;

            yield return new WaitForSeconds(0.2f);
        }
    }
}
