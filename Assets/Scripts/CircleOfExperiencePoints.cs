using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleOfExperiencePoints : MonoBehaviour
{
    [SerializeField] private Image _experienceCircle;
    [SerializeField] private Player _player;

    private void Start()
    {
        _experienceCircle.fillAmount = 0;
    }

    private void OnEnable()
    {
        _player.ExperienceChanged += OnExperienceChanged;
    }

    private void OnDisable()
    {
        _player.ExperienceChanged -= OnExperienceChanged;
    }

    private void OnExperienceChanged(float changedEX)
    {
        _experienceCircle.fillAmount = _player.CurrentXPImagineCirclePercent;
    }

}
