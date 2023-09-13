using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnNotification : MonoBehaviour
{
    private const string Notification = "NotificationObjectCreate";

    [SerializeField] private TMP_Text _gearWheelText;

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

    public void SpawnText(string text)
    {
        _gearWheelText.text = text;
    }
}
