using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LevelCounNotification : MonoBehaviour
{
    private const string LevelNotification = "LevelNotification";

    [SerializeField] private TMP_Text _textLevelPrefab;

    private Animator _levelAnimator;

    private void Start()
    {
        _levelAnimator = GetComponent<Animator>();
        _levelAnimator.Play(LevelNotification);
    }

    private void OnDisable()
    {
        _levelAnimator.StopPlayback();
    }

    public void LevelCountTextGet(int level)
    {
        _textLevelPrefab.text = level.ToString() + " level";
    }
    
    public void EndLevelAnimation()
    {
        Destroy(gameObject);
    }
}
