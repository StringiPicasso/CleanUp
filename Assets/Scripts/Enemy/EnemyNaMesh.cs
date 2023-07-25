using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNaMesh : MonoBehaviour
{
    [SerializeField] private Enemy _targetFromEnemy;

    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
       navMeshAgent.destination = _targetFromEnemy.TargetDestination;
    }
}
