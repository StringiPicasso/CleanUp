using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Agava.YandexGames;

public class InitSDK : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize(OnInitialized);
    }

    private void FixedUpdate()
    {
        Quaternion rotationX = Quaternion.AngleAxis(-_speed, Vector3.forward);
        transform.rotation *= rotationX;
    }

    private void OnInitialized()
    {
        SceneManager.LoadScene(1);
    }
}
