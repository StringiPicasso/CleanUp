using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChangeColor : MonoBehaviour
{
    [SerializeField] private Renderer[] _materials;

    private Color _enemyColor;

    public Color EnemyColor => _enemyColor;

    private void Awake()
    {
        _enemyColor = Random.ColorHSV(0, 1);

        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i].material.color = _enemyColor;
        }
    }
}
