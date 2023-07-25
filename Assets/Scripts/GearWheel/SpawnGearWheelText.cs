using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnGearWheelText : MonoBehaviour
{
    private const string Notification = "NotificationObjectCreate";

    [SerializeField] private TMP_Text _gearWheelText;
    
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

    public void GearWheelTextSpawn(string text)
    {
        _gearWheelText.text = text;
    }
}
