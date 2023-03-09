using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private MeshRenderer _material;

    private void Start()
    {
        _material.material.color = Random.ColorHSV(0, 1);
    }
}
