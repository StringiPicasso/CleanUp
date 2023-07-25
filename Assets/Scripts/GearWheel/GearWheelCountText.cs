using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GearWheelCountText : MonoBehaviour
{
    [SerializeField] private TMP_Text _gearWheelText;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private GearWheel _gearWheelPrefab;

    private void OnEnable()
    {
        _playerPrefab.GearWheelTaked += OnGearWheelTaked;
    }

    private void OnDisable()
    {
        _playerPrefab.GearWheelTaked -= OnGearWheelTaked;
    }

    private void OnGearWheelTaked()
    {
        _gearWheelText.text = _playerPrefab.CountGearWheel.ToString() + "/" + _gearWheelPrefab.NecessaryCountWheel.ToString();
    }
}
