using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorPlayer : MonoBehaviour
{
    [SerializeField] private Renderer[] _materials;
    [SerializeField] private StartGameSceen _colorPlayer;

    private void OnEnable()
    {
        _colorPlayer.ColorAlready += OnColorAlready;
    }

    private void OnColorAlready(Color color)
    {
        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i].material.color = color;
        }
    }
}
