using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _countMoveCameraAdidd;
    
    private float _distantY;
    private float _distantZ;

    private void Start()
    {
        _distantY = Camera.main.transform.position.y;
        _distantZ = Camera.main.transform.position.z;
    }

    private void Update()
    {
        transform.position = new Vector3(_player.transform.position.x,_player.transform.position.y+_distantY,_player.transform.position.z+ _distantZ);
    }
}
