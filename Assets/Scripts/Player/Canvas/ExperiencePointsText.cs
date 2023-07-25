using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]

public class ExperiencePointsText : MonoBehaviour
{

    private const string Notification = "Notification";
    private const string TextLextLevel = "Next Level";
    private const string TextLowLevel = "Low Level";
    private const string TextMaximumBonus = "Maximum";

    [SerializeField] private TMP_Text _experienceCountText;

    private Color _defaultColor;
    private Animator _animator;

    private void Awake()
    {
        _defaultColor = _experienceCountText.color;
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.Play(Notification);
    }

    private void OnDisable()
    {
        _animator.StopPlayback();
    }

    public void AnimationEnd()
    {
        gameObject.SetActive(false);
    }

    public void NotificationNewReward(int reward)
    {
        _experienceCountText.text = "+" + reward.ToString();
    }

    public void NotificationLostReward(int reward)
    {
        _experienceCountText.color = Color.red;
        _experienceCountText.text = "-" + reward.ToString();
        _experienceCountText.color = _defaultColor;
    }

    public void NotificationNextLevel()
    {
        _experienceCountText.text = TextLextLevel;
    }

    public void NotificationLowLevel()
    {
        _experienceCountText.text = TextLowLevel;
    }

    public void NotificationMaximumBonus()
    {
        _experienceCountText.text = TextMaximumBonus;
    }
}
