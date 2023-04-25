using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        transform.LookAt(_enemy.Target.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, _enemy.Target.transform.position, _speed * Time.deltaTime);
    }
}
