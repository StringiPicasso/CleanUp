using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TMP_Text _youEatText;
    [SerializeField] private TMP_Text _youBrokeText;

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void OnEatSceenOff()
    {
        _youEatText.gameObject.SetActive(false);
    }

    public void OnBrokeSceenOn()
    {
        _youBrokeText.gameObject.SetActive(true);
    }
}

