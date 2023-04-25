using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArrowOnAnotheerPlayers : MonoBehaviour
{
    [SerializeField] private Image _arrow;
    [SerializeField] private Transform _target;

    private void Update()
    {
        _arrow.transform.position = Camera.main.WorldToScreenPoint(_target.position);
    }
}
