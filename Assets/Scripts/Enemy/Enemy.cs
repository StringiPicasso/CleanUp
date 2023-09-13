using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Enemy : VacuumCleaner
{
    [SerializeField] private EnemiesNames _changeName;
    [SerializeField] private NavMeshAgent _movementEnemy;
    [SerializeField] private float _range;
    [SerializeField] private float _timeToDie;

    private Player _playerTarget;
    private Enemy _currentTarget;
    private Vector3 _target;
    private EnemyChangeColor _enemyChangeColor;
    private Coroutine _activeCoroutineMoveEnemy;

    public Vector3 TargetDestination => _target;
    public int LevelEnemy => _level;

    public event UnityAction<string> ExperienceTaked;
    public event UnityAction<VacuumCleaner> EnemySpawned;
    public event UnityAction EnemyDied;

    private void Start()
    {
        _namePlayerText.text = _changeName.ChangeName();
        _enemyChangeColor = GetComponent<EnemyChangeColor>();
        _namePlayerText.color = _enemyChangeColor.EnemyColor;
        _playerTarget = FindObjectOfType<Player>();
        EnemySpawned?.Invoke(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Trash>(out Trash trash))
        {
            _audioSource.PlayOneShot(_eatSound);
            ChangeExerience(trash.RewardTrash);
            Destroy(trash.gameObject);
        }

        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            if (player.Level < _level)
            {
                _audioSource.PlayOneShot(_eatSound);
                ChangeExerience(player.Reward);
                player.PlayerDiedFromEnemy();
            }
        }

        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (enemy.Level < _level)
            {
                _audioSource.PlayOneShot(_eatSound);
                ChangeExerience(enemy.Reward);
                Destroy(enemy.gameObject);
            }
        }

        if (other.gameObject.TryGetComponent<GearWheel>(out GearWheel gearWheel))
        {
            Destroy(gearWheel.gameObject);
        }

        if (other.gameObject.TryGetComponent<Puddle>(out Puddle puddle))
        {
            _audioSource.PlayOneShot(_puddleOnSound);
            _electricDamage.enabled = true;
            LostExerience(puddle.Damage);
        }
    }
    
    public override void ChangeExerience(int reward)
    {
        TryGetReward(reward);

        if (_currentExperience >= _maxExperienceForLevel)
        {
            _sckaleScript.TakeNewObjectSize();
            _currentExperience = _minExperienceForLevel;
            _maxExperienceForLevel += _numberNewMaxExperienceAdded;
            _level++;
        }

        ExperienceTaked?.Invoke(NamePlayer);
    }
    public override void LostExerience(int lostPoint)
    {
        TryLostReward(lostPoint);

        if (_currentExperience < _minExperienceForLevel)
        {
            _maxExperienceForLevel -= _numberNewMaxExperienceAdded;
            _level--;
            _sckaleScript.TakeLooseObjectSize();
        }

        ExperienceTaked?.Invoke(NamePlayer);

        if (_totalExperienceForLiderboard <= 0)
        {
            EnemyDied?.Invoke();
            Destroy(gameObject,_timeToDie);
        }
    }

    private void Update()
    {
        SelectTarget();
    }

    private void SelectTarget()
    {
        if (Vector3.Distance(transform.position, _playerTarget.transform.position) < _range && _playerTarget.Level < _level)
         {
         _target = _playerTarget.gameObject.transform.position;
         }
        else
        {
            CheckWeekEnemy();
        }
    }

     public void TakeCharactersForEnemy(int level, int currentExp, Vector3 scale, int maxExpForLevel)
    {
        _level=level;
        _maxExperienceForLevel = maxExpForLevel;
        _currentExperience = currentExp;
        _totalExperienceForLiderboard = _currentExperience;
        _sckaleScript.TakeScaleValueForEnemy(scale);
    }

    private void SelectClosetTrash()
    {
        float distanceToCloseTrash = Mathf.Infinity;
        Trash[] allTrashes = FindObjectsOfType<Trash>();

        for (int i = 0; i < allTrashes.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, allTrashes[i].transform.position);

            if (distance < distanceToCloseTrash)
            {
                distanceToCloseTrash = distance;
                _target = allTrashes[i].gameObject.transform.position;
            }
        }
    }

    private void CheckWeekEnemy()
    {
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        for (int i = 0; i < allEnemies.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, allEnemies[i].transform.position);

            if (distance < _range && allEnemies[i].Level < _level)
            {
                _currentTarget = allEnemies[i];
                _target = allEnemies[i].gameObject.transform.position;
            }
        }

        if (_currentTarget == null)
        {
            SelectClosetTrash();
        }
    }

/*    public void OnEnemyTaked(float time)
    {
        _activeCoroutineMoveEnemy = StartCoroutine(FreezeMovement(time));
    }

    private IEnumerator FreezeMovement(float time)
    {
        GetComponent<EnemyNaMesh>().enabled = false;
        _movementEnemy.enabled = false;

        yield return new WaitForSeconds(time);

        _movementEnemy.enabled = true;

        if (_activeCoroutineMoveEnemy != null)
        {
            StopCoroutine(_activeCoroutineMoveEnemy);
        }
    }*/
}
