using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Pet : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _movementPet;
    [SerializeField] private float _timeLifePet;
    [SerializeField] private float _timeFreezePlayers;
    [SerializeField] private float _range;
    [SerializeField] private float _timeToChangeRandomTarget;
    [SerializeField] private float _minimumRandomNumber;
    [SerializeField] private float _maximumRandomNumber;
    [SerializeField] private float _timeSpawnPuddle;

    private Coroutine _activeCoroutinePet;
    private VacuumCleaner _targetVacuum;
    private Animator _animatorPet;
    private Vector3 _targetPet;

    public float TimeToSpawnPuddle => _timeSpawnPuddle;
    public Vector3 TargetFromPet => _targetPet;

    public event UnityAction<float> PlayerTaked;
    public event UnityAction<float> EnemyTaked;

    private void Start()
    {
        _animatorPet = GetComponent<Animator>();
        _targetVacuum = GetComponent<VacuumCleaner>();
        Destroy(gameObject, _timeLifePet);
        _animatorPet.GetBool("IsNeedSitting");
        StartCoroutine(ChangeRandomTarget());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            EnemyTaked?.Invoke(_timeFreezePlayers);
            CatchPlayer(enemy.gameObject);
        }

        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            if (player.IsGearsInStock == false)
            {
                PlayerTaked?.Invoke(_timeFreezePlayers);
                CatchPlayer(player.gameObject);
            }
            else
            {
                player.CheskStockGears();
            }
        }
    }

    private void Update()
    {
        SelectTarget();

        if (Vector3.Distance(transform.position, _targetPet) < _range)
        {
            SelectRandomTarget();
        }
    }

    public void NewLevelCat(float newLife)
    {
        _timeLifePet += newLife;
    }

    private void CatchPlayer(GameObject player)
    {
        _activeCoroutinePet = StartCoroutine(FreezePlayerCoroutine(player));
    }

    private void SelectRandomTarget()
    {
        _targetPet = new Vector3(Random.Range(-_minimumRandomNumber, _maximumRandomNumber), 0, Random.Range(-_minimumRandomNumber, _maximumRandomNumber));
    }

    private void SelectTarget()
    {
        if (_targetPet == null)
        {
            if (Vector3.Distance(transform.position, _targetVacuum.gameObject.transform.position) < _range)
            {
                _targetPet = _targetVacuum.gameObject.transform.position;
            }
            else
            {
                SelectRandomTarget();
            }
        }
    }

    private IEnumerator ChangeRandomTarget()
    {
        yield return new WaitForSeconds(_timeToChangeRandomTarget);
        SelectRandomTarget();
    }

    private IEnumerator FreezePlayerCoroutine(GameObject player)
    {
        GetComponent<PetNavMEsh>().enabled = false;
        _movementPet.enabled = false;
        _targetPet = player.gameObject.transform.position;
        _animatorPet.SetBool("IsNeedSitting", true);
        _animatorPet.Play("sit");

        yield return new WaitForSeconds(_timeFreezePlayers);

        if (_activeCoroutinePet != null)
        {
            StopCoroutine(_activeCoroutinePet);
            _animatorPet.SetBool("IsNeedSitting", false);
            _animatorPet.Play("walk");
            GetComponent<PetNavMEsh>().enabled = true;
            _movementPet.enabled = true;
        }
    }
}
