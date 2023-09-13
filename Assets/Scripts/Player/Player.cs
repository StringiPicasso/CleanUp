using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(Animator))]

public class Player : VacuumCleaner
{
    [SerializeField] private KillsNumber _killsCountText;
    [SerializeField] private Puddle _lostPointPuddle;
    [SerializeField] private PlayerMovement _movePlayerScript;
    [SerializeField] private SpawnPet _perSpawn;
    [SerializeField] protected AudioClip _gearWheelPickUpSound;
    [SerializeField] protected AudioClip _lostWheelSound;
    [SerializeField] private InstantiateSpawnTextCanvas _spawnNotification;
    [SerializeField] private GameManagerCanvas _WheelsCount;
   // [SerializeField] private int _necessaryCountWheel;

    private Coroutine _activeCoroutineMove;
    private float _maxPercent = 100;
    private float _currentXPPercent;
    private float _currentImageCirclePercent;
    private int _totalKills = 0;
    private int _countGears = 0;
    private bool _isGearsInStock = false;

    public event UnityAction<float> ExperienceChanged;
    public event UnityAction<string> TotalExperienceTaked;
    public event UnityAction GearWheelChanged;
    public event UnityAction TimeToSpawnWheelCame;
    public event UnityAction EnemyKilled;
    public event UnityAction<VacuumCleaner> PlayerReady;

    public event UnityAction PlayerBroked;
    public event UnityAction PlayerAte;
   // public int NecessaryCountWheel => _necessaryCountWheel;

    public bool IsGearsInStock => _isGearsInStock;
    public int TotalKillsPlayer => _totalKills;
    public int CountGearWheel => _countGears;
    public float CurrentXPImagineCirclePercent => _currentImageCirclePercent;
    public int MaxExperienceForLevel => _maxExperienceForLevel;

    private void OnEnable()
    {
        _perSpawn.PetSpawned += PetSpawnEvent;
        PlayerReady?.Invoke(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Trash>(out Trash trash))
        {
            _audioSource.PlayOneShot(_eatSound);
            ChangeExerience(trash.RewardTrash);
            Destroy(trash.gameObject);
        }

        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (enemy.Level < _level)
            {
                _audioSource.PlayOneShot(_eatSound);

                _totalKills++;
                ChangeExerience(enemy.Reward);
                Destroy(enemy.gameObject);
                EnemyKilled?.Invoke();
            }
        }

        if (other.gameObject.TryGetComponent<Puddle>(out Puddle puddle))
        {
            _audioSource.PlayOneShot(_puddleOnSound);
            _electricDamage.enabled = true;
            LostExerience(puddle.Damage);
        }

        if (other.gameObject.TryGetComponent<GearWheel>(out GearWheel gearWheel))
        {
            if (_countGears == _WheelsCount.NecessaryCountWheel)
            {
                _spawnNotification.CreateLevelPointNotificationOnDisplay("Maximum");
            }
            else
            {
                _audioSource.PlayOneShot(_gearWheelPickUpSound);

                Destroy(gearWheel.gameObject);
                _countGears++;
                _isGearsInStock = true;
                GearWheelChanged?.Invoke();
            }
        }
    }

    public override void ChangeExerience(int reward)
    {
        TryGetReward(reward);

        if (_currentExperience >= _maxExperienceForLevel)
        {
            _spawnNotification.CreateLevelPointNotificationOnDisplay("Next Level");
            _sckaleScript.TakeNewObjectSize();
            _currentExperience = _minExperienceForLevel;
            _maxExperienceForLevel += _numberNewMaxExperienceAdded;
            _level++;
        }

        TotalExperienceTaked?.Invoke(NamePlayer);
        ChangeExperienceCount(reward);
        _spawnNotification.CreateLevelPointNotificationOnDisplay(" + " + reward.ToString());
        TimeToSpawnWheelCame?.Invoke();
    }

    public override void LostExerience(int lostPoint)
    {
        TryLostReward(lostPoint);

        if (_currentExperience < _minExperienceForLevel)
        {
            _spawnNotification.CreateLevelPointNotificationOnDisplay("Low Level");
            _maxExperienceForLevel -= _numberNewMaxExperienceAdded;
            _level--;

             _sckaleScript.TakeLooseObjectSize();
        }

        TotalExperienceTaked?.Invoke(NamePlayer);
        ChangeExperienceCount(lostPoint);
        _spawnNotification.CreateLevelPointNotificationOnDisplay(" - " + lostPoint.ToString());
        TimeToSpawnWheelCame?.Invoke();

        if (_totalExperienceForLiderboard < 0)
        {
            PlayerBroked?.Invoke();
        }
    }

    public void PlayerDiedFromEnemy()
    {
        PlayerAte?.Invoke();
    }

    public void CheskStockGears()
    {
        _audioSource.PlayOneShot(_lostWheelSound);
        _countGears--;
        GearWheelChanged?.Invoke();

        if (_countGears <= 0)
        {
            _isGearsInStock = false;
        }
    }

    public void OnPlayerTaked(float time)
    {
        _activeCoroutineMove = StartCoroutine(FreezeMovement(time));
    }

    public void PetSpawnEvent(Pet pet)
    {
        pet.PlayerTaked += OnPlayerTaked;
    }

    public void LostPointFromBoss(int lostPoint)
    {
        LostExerience(lostPoint);
    }

    private void ChangeExperienceCount(int reward)
    {
        _killsCountText.TotalKillsCount(_totalKills);
        CalculateCurrentPercent(reward);
        ExperienceChanged?.Invoke(CurrentXPImagineCirclePercent);
    }

    private void CalculateCurrentPercent(int reward)
    {
        _currentXPPercent = (_currentExperience * _maxPercent) / _maxExperienceForLevel;
        _currentImageCirclePercent = _currentXPPercent / _maxPercent;
    }

    private IEnumerator FreezeMovement(float time)
    {
        _movePlayerScript.enabled = false;

        yield return new WaitForSeconds(time);

        _movePlayerScript.enabled = true;

        if (_activeCoroutineMove != null)
        {
            StopCoroutine(_activeCoroutineMove);
        }
    }
}
