using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetPursue : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _range;
    [SerializeField] private Player _player;

    private void Update()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) > _range||_player.Level>GetComponent<Enemy>().LevelEnemy)
        {
            gameObject.GetComponent<PlayerTargetPursue>().enabled = false;
        }

        transform.LookAt(_player.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
    }
}
