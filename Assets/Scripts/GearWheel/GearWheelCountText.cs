using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GearWheelCountText : MonoBehaviour
{
    [SerializeField] private TMP_Text _gearWheelText;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private GameManagerCanvas _gearWheelPrefab;

    private Animator _wheelAnimator;

    private void Start()
    {
        _wheelAnimator = GetComponent<Animator>();
        _wheelAnimator.GetBool("IsChangeWheels");
        _gearWheelText.text = 0.ToString()+ "/" + _gearWheelPrefab.NecessaryCountWheel.ToString();
    }

    private void OnEnable()
    {
        _playerPrefab.GearWheelChanged += OnGearWheelTaked;
        _gearWheelPrefab.WheelCountChanged += OnGearWheelTaked;
    }

    private void OnDisable()
    {
        _playerPrefab.GearWheelChanged -= OnGearWheelTaked;
        _gearWheelPrefab.WheelCountChanged -= OnGearWheelTaked;
    }

    public void AnimationEndWheels()
    {
        _wheelAnimator.SetBool("IsChangeWheels", false);
    }

    private void OnGearWheelTaked()
    {
        _wheelAnimator.SetBool("IsChangeWheels",true);

        _gearWheelText.text = _playerPrefab.CountGearWheel.ToString() + "/" + _gearWheelPrefab.NecessaryCountWheel.ToString();
    }
}
