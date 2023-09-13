using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockroachMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _range;
    [SerializeField] private float _timeToChangeRandomTarget;

    private Vector3 _target;
    private LocationSize _area;

    private void Start()
    {
        _area=FindObjectOfType<LocationSize>();
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
        _target = _area.ChooseRandomSpawnPozitionXZ(0);
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