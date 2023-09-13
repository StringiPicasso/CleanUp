using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]

public abstract class VacuumCleaner : MonoBehaviour
{
    private const string PickUpObject = "PickUpTrash";
    
    [SerializeField] Transform _sitForCatPosition;
    [SerializeField] protected int _rewardValue;
    [SerializeField] protected int _maxExperienceForLevel;
    [SerializeField] protected TMP_Text _namePlayerText;
    [SerializeField] protected AnimationEdit _sckaleScript;
    [SerializeField] protected EcelcticDamage _electricDamage;
    [SerializeField] protected AudioClip _eatSound;
    [SerializeField] protected AudioClip _collisionSound;
    [SerializeField] protected AudioClip _puddleOnSound;
    [SerializeField] protected AudioSource _audioSource;

    protected Animator _animator;
    protected int _minExperienceForLevel = 0;
    protected int _level = 1;
    protected int _currentExperience;
    protected int _numberNewMaxExperienceAdded = 20;
    protected int _totalExperienceForLiderboard;

    public Transform SitForCatPosition => _sitForCatPosition;
    public int Level => _level;
    public int Reward => _rewardValue;
    public string NamePlayer => _namePlayerText.text;
    public int TotalExperienceForLiderboard => _totalExperienceForLiderboard;

    public abstract void ChangeExerience(int reward);
    public abstract void LostExerience(int lostPoint);

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void TryGetReward(int reward)
    {
        _totalExperienceForLiderboard += reward;
        _currentExperience += reward;
        _animator.Play(PickUpObject);
    }

    public void TryLostReward(int lostPoint)
    {
        _totalExperienceForLiderboard -= lostPoint;
        _currentExperience -= lostPoint;
    }
}
