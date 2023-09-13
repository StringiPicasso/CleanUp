using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Puddle : MonoBehaviour
{
    [SerializeField] private float _timeToDie;
    [SerializeField] private int _minScale;
    [SerializeField] private int _maxScale;

    private Animator _animatorPuddle;
    private int _currentScalePuddle;
    private int _damage;
    private int _currentDamageMin;
    private int _currentDamageMax;

    public int Damage => _damage;
    public int CurrentMaxDamagePuddle => _currentDamageMax;
    public int CurrentMinDamagePuddle => _currentDamageMin;

    void Start()
    {
        _damage = Random.Range(_currentDamageMin, _currentDamageMax);
        _currentScalePuddle = Random.Range(_minScale, _maxScale);
        transform.localScale *= _currentScalePuddle;
       
        _animatorPuddle = GetComponent<Animator>();
        _animatorPuddle.Play("Emerge");

        Destroy(gameObject, _timeToDie);
    }

    public void FirstSpawnFromCat(int min, int max)
    {
        _currentDamageMin = min;
        _currentDamageMax = max;
    }

    public void NewLevelPuddle(float newLife,int newMaxDamage,int newMinDamage)
    {
        _timeToDie += newLife;
        _currentDamageMax = newMaxDamage;
        _currentDamageMin = newMinDamage;
    }
}
