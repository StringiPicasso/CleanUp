using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]

public class Player : VacuumCleaner
{
    [SerializeField] private KillsNumber _killsCountText;
    [SerializeField] private StartGameSceen _nameForPlayer;
    [SerializeField] private Puddle _lostPointPuddle;
    [SerializeField] private GameOver _imGameOver;
    [SerializeField] private EnemySpawn _prefabForLeaderboard;
    [SerializeField] private PlayerMovement _movePlayerScript;
    [SerializeField] private SpawnPet _perSpawn;

    private Coroutine _activeCoroutineMove;

    private float _maxPercent = 100;
    private float _currentXPPercent;
    private float _currentImageCirclePercent;
    private int _totalKills = 0;
    private int _countGears = 0;
    private bool _isGearsInStock = false;

    public event UnityAction<float> ExperienceChanged;
    public event UnityAction TotalExperienceTaked;
    public event UnityAction GearWheelTaked;
    public event UnityAction ExperienceTakedForCheckBonus;
    public event UnityAction EnemyKilled;
    public event UnityAction<VacuumCleaner> PlayerReady;

    public bool IsGearsInStock => _isGearsInStock;
    public int TotalExperiencePlayer => _totalExperience;
    public int TotalKillsPlayer => _totalKills;
    public int CountGearWheel => _countGears;
    public float CurrentXPImagineCirclePercent => _currentImageCirclePercent;

    private void OnEnable()
    {
        _nameForPlayer.nameAlready += OnNameAlready;
        _perSpawn.PetSpawned += PetSpawnEvent;
        PlayerReady?.Invoke(this);
    }

    private void OnNameAlready(string name)
    {
        _namePlayerText.text = name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Trash>(out Trash trash))
        {
            ChangeExerience(trash.RewardTrash);
            Destroy(trash.gameObject);
        }

        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (enemy.Level < _level)
            {
                _totalKills++;
                ChangeExerience(enemy.Reward);
                Destroy(enemy.gameObject);
                EnemyKilled?.Invoke();
            }
        }

        if (other.gameObject.TryGetComponent<Puddle>(out Puddle puddle))
        {
            _activeCoroutinePuddle = StartCoroutine(RaseLostPoint());
            LostExerience(_lostPoint);
        }

        if (other.gameObject.TryGetComponent<GearWheel>(out GearWheel gearWheel))
        {
            if (_countGears == gearWheel.NecessaryCountWheel)
            {
                _notificationNewReward.gameObject.SetActive(true);
                _notificationNewReward.NotificationMaximumBonus();
            }
            else
            {
                Destroy(gearWheel.gameObject);
                _countGears++;
                _isGearsInStock = true;
                GearWheelTaked?.Invoke();
            }
        }
    }

    public override void ChangeExerience(int reward)
    {
        TryGetReward(reward);
        TotalExperienceTaked?.Invoke();
        ChangeExperienceCount(reward);
        NotificationReceivePOints(reward);
        ExperienceTakedForCheckBonus?.Invoke();
    }
    public override void LostExerience(int lostPoint)
    {
        OnLostPoint(lostPoint);

        if (_totalNumberPointsExperience < 0)
        {
            _imGameOver.OnEatSceenOff();
            _imGameOver.OnBrokeSceenOn();
            _imGameOver.gameObject.SetActive(true);
        }
    }

    public void PlayerDiedFromEnemy()
    {
        _imGameOver.gameObject.SetActive(true);
    }

    public void CheskStockGears()
    {
        _countGears--;
        GearWheelTaked?.Invoke();

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

    private IEnumerator FreezeMovement(float time)
    {
        _movePlayerScript.enabled = false;

        yield return new WaitForSeconds(time);

        _movePlayerScript.enabled = true;

        if (_activeCoroutinePuddle != null)
        {
            StopCoroutine(_activeCoroutineMove);
        }
    }
    private void OnLostPoint(int lostPoint)
    {
        TryLostReward(lostPoint);
        TotalExperienceTaked?.Invoke();
        ChangeExperienceCount(lostPoint);
        NotificationLowPOints(lostPoint);
        ExperienceTakedForCheckBonus?.Invoke();
    }

    private void ChangeExperienceCount(int reward)
    {
        _killsCountText.TotalKillsCount(_totalKills);
        CalculateCurrentPercent(reward);
        ExperienceChanged?.Invoke(CurrentXPImagineCirclePercent);
    }

    private void CalculateCurrentPercent(int reward)
    {
        _currentXPPercent = (_totalExperience * _maxPercent) / _maxExperience;
        _currentImageCirclePercent = _currentXPPercent / _maxPercent;
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

    private void NotificationLooseReward(int reward)
    {
        _notificationNewReward.gameObject.SetActive(true);
        _notificationNewReward.NotificationLostReward(reward);
    }

    private void NotificationLowPOints(int reward)
    {
        NotificationLooseReward(reward);
    }
}
