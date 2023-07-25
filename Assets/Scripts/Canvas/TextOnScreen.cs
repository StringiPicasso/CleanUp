using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextOnScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _enemyCanvas;

    void Update()
    {
        _enemyCanvas.transform.LookAt(Camera.main.transform);
        _enemyCanvas.transform.Rotate(0, 180, 0);
    }
}
