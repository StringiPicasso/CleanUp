using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bl_Joystick _joystick;
    [SerializeField] private float _speed;

    private Rigidbody _playerRigidbody;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _playerRigidbody.velocity = new Vector3(_joystick.Horizontal * _speed, _playerRigidbody.velocity.y, _joystick.Vertical * _speed);
            transform.rotation = Quaternion.LookRotation(_playerRigidbody.velocity);
        }
    }
}
