using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _speedMove;
    [SerializeField] private float _speedRotation;

    private Rigidbody _playerRigidbody;
    private Vector3 _moveVector;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
         _moveVector = Vector3.zero;
         _moveVector.x = _joystick.Horizontal * _speedMove * Time.deltaTime;
         _moveVector.z = _joystick.Vertical * _speedMove * Time.deltaTime;

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, _moveVector, _speedRotation * Time.deltaTime, 0);
            transform.rotation = Quaternion.LookRotation(direction);
        }

        _playerRigidbody.velocity = new Vector3(_joystick.Horizontal * _speedMove, 0, _joystick.Vertical * _speedMove);
    }
}
