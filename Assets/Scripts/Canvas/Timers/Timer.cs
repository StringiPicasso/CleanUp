using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _timeGame;
    [SerializeField] private TMP_Text _timeDisplay;

    public event UnityAction TimeGameFinished;

    private void Update()
    {
        if (_timeGame <= 0)
        {
            TimeGameFinished?.Invoke();
        }
        else
        {
            _timeGame -= Time.deltaTime;
            System.TimeSpan time = System.TimeSpan.FromSeconds(_timeGame);
            _timeDisplay.text = time.Minutes.ToString("00") + ":" + time.Seconds.ToString("00");
        }
    }
}
