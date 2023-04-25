using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private int _countEnemy;
    [SerializeField] private int _miminumRandomNumber;
    [SerializeField] private int _maximumRandomNumber;

    private Vector3 _randomWpawnPositionEnemy;

    private Enemy _target;

    public Enemy TargetEnemy => _target;

    public event UnityAction<Enemy> EnemyCreated;

    private void Update()
    {
        if (_countEnemy > 0)
        {
            InstantiateEnemy();
            _countEnemy--;
        }
    }

    private void InstantiateEnemy()
    {
        _randomWpawnPositionEnemy = new Vector3(Random.Range(-_miminumRandomNumber, _maximumRandomNumber), _enemy.transform.position.y, Random.Range(-_miminumRandomNumber, _maximumRandomNumber));
        _target=Instantiate(_enemy, _randomWpawnPositionEnemy, Quaternion.identity);
        EnemyCreated?.Invoke(_target);


      //  enemy.Init(_player);
      //  enemy.EnemyDying += OnEnemyDying;
    }
/*
    private void OnEnemyDying(Enemy enemy)
    {
        enemy.EnemyDying -= OnEnemyDying;

        _player.AddMoney(enemy.Reward);
    }*/
}

