using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnPointNotification : MonoBehaviour
{
    private const string Notification = "Notification";

    [SerializeField] private TMP_Text _experienceCountText;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.Play(Notification);
    }

    private void OnDisable()
    {
        _animator.StopPlayback();
    }

    public void AnimationEnd()
    {
        Destroy(gameObject);
    }

    public void SpawnNewNotification(string textLevel)
    {
        _experienceCountText.text = textLevel;
    }
}
