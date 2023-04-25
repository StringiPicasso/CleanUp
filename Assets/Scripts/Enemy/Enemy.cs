using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : VacuumCleaner
{
    [SerializeField] private float _range;
    [SerializeField] private int _reward;
    [SerializeField] private SpawnTrash _possibleTarget;
    [SerializeField] private Player player;
    [SerializeField] private PlayerTargetPursue _targetPlayer;
    [SerializeField] private TrashTargetPursue _targetTrash;
    [SerializeField] private EnemySpawn _targetEnemy;
    [SerializeField] private EnemyMove _move;

    private Trash _target;
    public int Reward => _reward;
    public Trash Target => _target;

    public int LevelEnemy => _level;

    public event UnityAction<Enemy> EnemyDying;

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
    }

    public override void ChangeExerience(int reward)
    {
        TryGetReward(reward);
    }

    private void Update()
    {
        if (_target == null)
        {
            SelectTarget();
        }
    }

    private void SelectTarget()
    {
        if (Vector3.Distance(transform.position, player.gameObject.transform.position) < _range && player.Level < _level)
        {
            _move.enabled = false;
            _targetPlayer.enabled = true;
        }
        else
        {
            _target = _possibleTarget.TargetTrash;
           _move.enabled = true;
        }
    }
}
