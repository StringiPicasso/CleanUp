using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManagerCanvas : MonoBehaviour
{
    static private Color _playerColor;

    [SerializeField] private AdvManagement _adOpen;
    [SerializeField] private GameComplete _gameComplete;
    [SerializeField] private GameOver _gameOver;
    [SerializeField] private Player _player;
    [SerializeField] private Timer _timer;
    [SerializeField] private int _necessaryCountWheel;
    [SerializeField] private LevelCounNotification _levelNotificationPrefab;
    [SerializeField] private Transform _levelPrefabPlace;

    private int _globalPointPlayer;
  //  private int _globalKillPlayer;
    private int _firstLevelGame = 1;
    private int _currentLevel;

    public int GlobalPointPlayer => _globalPointPlayer;
    public int NecessaryCountWheel => _necessaryCountWheel;

    public event UnityAction WheelCountChanged;
    public event UnityAction<Color> ColorAlready;

    private void OnEnable()
    {

        _player.PlayerBroked += OnPlayerBroked;
        _player.PlayerAte += OnPlayerAte;
        _timer.TimeGameFinished += OnTimeGameFinished;
        _adOpen.WheelsChanged += OnWheelsChanged;
    }

    private void OnDisable()
    {
        _player.PlayerBroked -= OnPlayerBroked;
        _player.PlayerAte -= OnPlayerAte;
        _timer.TimeGameFinished -= OnTimeGameFinished;
        _adOpen.WheelsChanged -= OnWheelsChanged;
    }

    private void Start()
    {
        CheckLevelData();

        var currentLevelPrefab = Instantiate(_levelNotificationPrefab, _levelPrefabPlace);
        currentLevelPrefab.LevelCountTextGet(_currentLevel);
    }

    public void NextLevelButton()
    {
        _adOpen.OnShowAd();
        NextLevel();
    }

    public void RestartButton()
    {
        RestartGame();
    }

    public void NewGame()
    {
        LoadFirstLevelData();
        SceneManager.LoadScene(_currentLevel);
    }

    public void SaveColorPlayer(Color color)
    {
        SaveColor(color, "Color");
        _playerColor = color;
        ColorAlready?.Invoke(_playerColor);
    }

    public static void SaveColor(Color color, string key)
    {
        PlayerPrefs.SetFloat(key + "R", color.r);
        PlayerPrefs.SetFloat(key + "G", color.g);
        PlayerPrefs.SetFloat(key + "B", color.b);
        PlayerPrefs.SetFloat(key + "A", color.a);
    }

    public static Color GetSaveColor(string key)
    {
        float r = PlayerPrefs.GetFloat(key + "R");
        float g = PlayerPrefs.GetFloat(key + "G");
        float b = PlayerPrefs.GetFloat(key + "B");
        float a = PlayerPrefs.GetFloat(key + "A");

        return new Color(r, g, b, a);
    }

    static private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void NextLevel()
    {
        _currentLevel++;
        PlayerPrefs.SetInt("level", _currentLevel);
        SceneManager.LoadScene(_currentLevel);
    }

    private void CheckLevelData()
    {
        _currentLevel = PlayerPrefs.GetInt("level");

        if (_currentLevel <= _firstLevelGame)
        {
            LoadFirstLevelData();
        }
        else
        {
            _globalPointPlayer = PlayerPrefs.GetInt("points");
           // _globalKillPlayer = PlayerPrefs.GetInt("kills");
            _necessaryCountWheel = PlayerPrefs.GetInt("WheelsCount");
            _playerColor = GetSaveColor("Color");
            ColorAlready?.Invoke(_playerColor);
        }
    }

    private void LoadFirstLevelData()
    {
        PlayerPrefs.SetInt("WheelsCount", _necessaryCountWheel);
        _globalPointPlayer = 0;
       // _globalKillPlayer = 0;
        _currentLevel = _firstLevelGame;
        PlayerPrefs.SetInt("points", _globalPointPlayer);
      //  PlayerPrefs.SetInt("kills", _globalKillPlayer);
        PlayerPrefs.SetInt("level", _currentLevel);
    }

    private void OnWheelsChanged(int newCount)
    {
        _necessaryCountWheel = newCount;
        PlayerPrefs.SetInt("WheelsCount", _necessaryCountWheel);
        WheelCountChanged?.Invoke();
    }

    private void PrefPlayerSave()
    {
        _globalPointPlayer += _player.TotalExperienceForLiderboard;
       // _globalKillPlayer += _player.TotalKillsPlayer;
        PlayerPrefs.SetInt("points", _globalPointPlayer);
       // PlayerPrefs.SetInt("kills", _globalKillPlayer);
        PlayerPrefs.Save();
    }

    private void OnPlayerBroked()
    {
        PrefPlayerSave();
        _gameOver.OnEatSceenOff();
        _gameOver.OnBrokeSceenOn();
        _gameOver.gameObject.SetActive(true);
    }

    private void OnPlayerAte()
    {
        PrefPlayerSave();

        _gameOver.gameObject.SetActive(true);
    }

    private void OnTimeGameFinished()
    {
        PrefPlayerSave();

        _gameComplete.gameObject.SetActive(true);
    }
}
