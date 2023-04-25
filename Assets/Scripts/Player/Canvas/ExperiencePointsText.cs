using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]

public class ExperiencePointsText : MonoBehaviour
{
    private const string Notification = "Notification";
    private const string TextLextLevel = "Next Level";

    [SerializeField] private TMP_Text _experienceCountText;

    private Animator _animator;

    private void Awake()
    {
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
       _experienceCountText.text = "+"+reward.ToString();
    }
   
    public void NotificationNextLevel()
    {
        _experienceCountText.text = TextLextLevel;
    }
}
