using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetNavMEsh : MonoBehaviour
{
    [SerializeField] private Pet _targetFromPet;

    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        navMeshAgent.destination = _targetFromPet.TargetFromPet;
    }
}
