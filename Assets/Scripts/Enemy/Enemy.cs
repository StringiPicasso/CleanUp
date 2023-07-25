using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.AI;

public class Enemy : VacuumCleaner
{
    [SerializeField] private EnemiesNames _changeName;
    [SerializeField] private TMP_Text _enemyNameText;
    [SerializeField] private NavMeshAgent _movementEnemy;
    [SerializeField] private SpawnPet _perSpawn;
    [SerializeField] private float _range;
    
    private Player _playerTarget;
    private Enemy _currentTarget;
    private Vector3 _target;
    private EnemyChangeColor _enemyChangeColor;
    private Coroutine _activeCoroutineMoveEnemy;

    public Vector3 TargetDestination => _target;
    public float EnemyRange => _range;
    public int LevelEnemy => _level;

    public event UnityAction ExperienceTaked;
    public event UnityAction EnemyKilled;
    public event UnityAction<VacuumCleaner> EnemySpawned;

    private void Start()
    {
        _perSpawn.PetSpawned += PetSpawnEvent;
        _enemyNameText.text = _changeName.ChangeName();
        _enemyChangeColor = GetComponent<EnemyChangeColor>();
        _enemyNameText.color = _enemyChangeColor.EnemyColor;
        _playerTarget = FindObjectOfType<Player>();
        EnemySpawned?.Invoke(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Trash>(out Trash trash))
        {
            ChangeExerience(trash.RewardTrash);
            Destroy(trash.gameObject);
        }
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            if (player.Level < _level)
            {
                ChangeExerience(player.Reward);
                player.PlayerDiedFromEnemy();
            }
        }
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (enemy.Level < _level)
            {
                ChangeExerience(enemy.Reward);
                Destroy(enemy.gameObject);
                EnemyKilled?.Invoke();
            }
        }

        if (other.gameObject.TryGetComponent<GearWheel>(out GearWheel gearWheel))
        {
            Destroy(gearWheel.gameObject);
        }

        if (other.gameObject.TryGetComponent<Puddle>(out Puddle puddle))
        {
            _activeCoroutinePuddle = StartCoroutine(RaseLostPoint());

            LostExerience(_lostPoint);
        }
    }
    
    public override void ChangeExerience(int reward)
    {
        TryGetReward(reward);
        ExperienceTaked?.Invoke();
    }
    public override void LostExerience(int lostPoint)
    {
        TryLostReward(lostPoint);
        ExperienceTaked?.Invoke();

        if (_totalNumberPointsExperience < 0)
        {
            Destroy(gameObject);
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

    public void PetSpawnEvent(Pet pet)
    {
        Debug.Log("Enemy Taked");
        pet.EnemyTaked += OnEnemyTaked;

    }

    public void OnEnemyTaked(float time)
    {
        _activeCoroutineMoveEnemy = StartCoroutine(FreezeMovement(time));
    }

    private IEnumerator FreezeMovement(float time)
    {
        GetComponent<EnemyNaMesh>().enabled = false;
        _movementEnemy.enabled = false;

        yield return new WaitForSeconds(time);

        _movementEnemy.enabled = true;

        if (_activeCoroutinePuddle != null)
        {
            StopCoroutine(_activeCoroutineMoveEnemy);
        }
    }
}
