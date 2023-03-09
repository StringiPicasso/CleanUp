using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _timeRemaining;
    [SerializeField] private TMP_Text _timeDisplay;

    private void Update()
    {
        if (_timeRemaining > 0)
        {
            _timeRemaining -= Time.deltaTime;
        }

        System.TimeSpan time = System.TimeSpan.FromSeconds(_timeRemaining);
        _timeDisplay.text = time.Minutes.ToString("00") + ":" + time.Seconds.ToString("00");
    }
}
