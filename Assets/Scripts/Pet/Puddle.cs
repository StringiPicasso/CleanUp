using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Puddle : MonoBehaviour
{
    [SerializeField] private float _timeToDie;

    private Animator _animatorPuddle;

    void Start()
    {
        _animatorPuddle = GetComponent<Animator>();
        _animatorPuddle.Play("Emerge");
        Destroy(gameObject, _timeToDie);
    }

    public void NewLifePuddle(float newLife)
    {
        _timeToDie += newLife;
    }
}
