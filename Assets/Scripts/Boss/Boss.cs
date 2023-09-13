using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    [SerializeField] private int _lostPointToPlayer;
    [SerializeField] private float _freezeTime;
    [SerializeField] private BossNavMesh _movementBoss;
    [SerializeField] private AudioClip _attackPlayers;
    [SerializeField] private AudioClip _triggerPlayers;
    [SerializeField] private AudioSource _audioSource;

    private Animator _bossAimator;
    private Coroutine _activeCoroutineBoss;
    private Coroutine _activeCoroutineKillerBoss;
    private Player _target;
    private float _killTIme;
    private float _lifeTime;
    private bool IsKillTime;

    public Player Target => _target;

    public event UnityAction TimeKillCame;
    
    private void Start()
    {
        IsKillTime = false;
        _bossAimator = GetComponent<Animator>();
        _bossAimator.GetBool("IsneedASit");
        _bossAimator.GetBool("IsNeedAAttack");
        _target = FindObjectOfType<Player>();
        _activeCoroutineKillerBoss = StartCoroutine(TimeDownToKiller());
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            if (player.IsGearsInStock == false)
            {
                _bossAimator.SetBool("IsNeedAAttack", true);
                _audioSource.PlayOneShot(_attackPlayers);

                AttackPlayer(player);
            }
            else
            {
                _bossAimator.SetBool("IsneedASit", true);
                _audioSource.PlayOneShot(_triggerPlayers);

                player.CheskStockGears();
                _activeCoroutineBoss = StartCoroutine(FreezeBoss(player));
            }
        }
    }

    public void ChangeBossCaracters(float lifeTime, float killTime)
    {
        _lifeTime = lifeTime;
        _killTIme = _lifeTime - killTime;
    }

    public void EndAnimationBoss()
    {
        _bossAimator.SetBool("IsNeedAAttack", false);
        _bossAimator.SetBool("IsneedASit", true);
    }

    private void AttackPlayer(Player player)
    {
        if (IsKillTime)
        {
            player.PlayerDiedFromEnemy();
        }
        else
        {
            if (_activeCoroutineBoss == null)
            {
                player.LostPointFromBoss(_lostPointToPlayer);
                _activeCoroutineBoss = StartCoroutine(FreezeBoss(player));
            }
        }
    }

    private IEnumerator TimeDownToKiller()
    {
        yield return new WaitForSeconds(_killTIme);

        IsKillTime = true;
        TimeKillCame?.Invoke();

        if (_activeCoroutineKillerBoss != null)
        {
            StopCoroutine(_activeCoroutineKillerBoss);
        }
    }

    private IEnumerator FreezeBoss(Player player)
    {
        _movementBoss.enabled = false;

        yield return new WaitForSeconds(_freezeTime);

        if (_activeCoroutineBoss != null)
        {
            StopCoroutine(_activeCoroutineBoss);
            _bossAimator.SetBool("IsneedASit", false);
            _bossAimator.Play("BossWalk");
            _activeCoroutineBoss = null;
            _movementBoss.enabled = true;

        }
    }
}
