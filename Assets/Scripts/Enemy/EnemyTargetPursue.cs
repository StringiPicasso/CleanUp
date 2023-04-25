using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetPursue : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _range;
    [SerializeField] private EnemySpawn _enemy;

    private void Update()
    {
        if (Vector3.Distance(transform.position, _enemy.TargetEnemy.transform.position) > _range || _enemy.TargetEnemy.Level > GetComponent<Enemy>().LevelEnemy)
        {
            gameObject.GetComponent<PlayerTargetPursue>().enabled = false;
        }

        transform.LookAt(_enemy.TargetEnemy.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, _enemy.TargetEnemy.transform.position, _speed * Time.deltaTime);
    }
}
