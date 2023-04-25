using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashTargetPursue : MonoBehaviour
{
    [SerializeField] private SpawnTrash _possibleTarget;
    [SerializeField] private float _speed;

    private Trash _currentTarget;

    private void Start()
    {
        _currentTarget = _possibleTarget.TargetTrash;
    }

    private void Update()
    {
        transform.LookAt(_currentTarget.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, _currentTarget.transform.position, _speed * Time.deltaTime);

        if (_currentTarget == null)
        {
           // gameObject.GetComponent<DistanceTrashTransite>().enabled = false;
        }
    }
}
