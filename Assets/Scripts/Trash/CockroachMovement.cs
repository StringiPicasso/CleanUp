using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockroachMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _range;
    [SerializeField] private float _timeToChangeRandomTarget;
    [SerializeField] private float _minimumRandomNumber;
    [SerializeField] private float _maximumRandomNumber;

    private Vector3 _target;

    private void Start()
    {
        StartCoroutine(ChangeRandomTarget());
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _target) < _range)
        {
            SelectRandomTarget();
        }

        Move();
    }

    private void SelectRandomTarget()
    {
        _target = new Vector3(Random.Range(-_minimumRandomNumber, _maximumRandomNumber), 0, Random.Range(-_minimumRandomNumber, _maximumRandomNumber));
    }

    private void Move()
    {
        transform.LookAt(_target);
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed*Time.deltaTime);
    }

    private IEnumerator ChangeRandomTarget()
    {
        yield return new WaitForSeconds(_timeToChangeRandomTarget);
        SelectRandomTarget();
    }
}