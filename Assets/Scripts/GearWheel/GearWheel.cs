using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearWheel : MonoBehaviour
{
    [SerializeField] private float _cycleLifeWheel;

    private Animator _animator;
   

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.Play("DefaultKey");

        Destroy(gameObject, _cycleLifeWheel);
    }
}
