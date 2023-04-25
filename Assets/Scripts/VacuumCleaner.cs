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
    [SerializeField] protected Rating _ratingg;
    [SerializeField] private TMP_Text _namePlayerText;


    protected Animator _animator;
    private Vector3 _numberNewLocalScaleAdded=new Vector3(0.3f,0.3f,0.3f);

    protected int _minExperience=0;
    protected int _level = 1;
    protected int _totalExperience;
    protected int _receivedReward;
    protected int _numberNewMaxExperienceAdded = 5;
    protected int _totalNumberPointsExperience;

    public int Level => _level;
    public string Name => _namePlayerText.text;
    public int TotalNumberPointExpirience=>_totalNumberPointsExperience;

    public abstract void ChangeExerience(int reward);

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void TryGetReward(int reward)
    {
        _totalNumberPointsExperience += reward;
        _receivedReward = reward;
        ChangeTotalExperience(_receivedReward);
    }

    private void ChangeTotalExperience(int reward)
    {
        _totalExperience += reward;
        _animator.SetTrigger(PickUpObject);
        TakeNewLevel();
    }

    private void TakeNewLevel()
    {
        if (_totalExperience >= _maxExperience)
        {
            TakeNewLevelText();
            TakeNewObjectSize();
            _totalExperience = _minExperience;
            _maxExperience += _numberNewMaxExperienceAdded;
            _level++;
        }
    }

    private void TakeNewLevelText()
    {
        _notificationNewReward.gameObject.SetActive(true);
        _notificationNewReward.NotificationNextLevel();
    }

    private void TakeNewObjectSize()
    {
        GetComponent<Animator>().enabled = false;
        transform.localScale += _numberNewLocalScaleAdded;
        _animator.enabled = true;
    }

}
