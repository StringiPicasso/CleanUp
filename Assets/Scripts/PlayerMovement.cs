using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _playerRigidbody;
    private Vector3 _touchPosition;
    private Vector3 _playerPosition;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            _playerRigidbody.MovePosition(transform.position + (transform.forward *_speed* Time.deltaTime));
              
            _touchPosition = touch.position;
            _playerPosition = Camera.main.WorldToScreenPoint(transform.position);
            _playerPosition = _touchPosition - _playerPosition;
            float angle = Mathf.Atan2(_playerPosition.y, _playerPosition.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(-angle, Vector3.up);
        }
    }
}
