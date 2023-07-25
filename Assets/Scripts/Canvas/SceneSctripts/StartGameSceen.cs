using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartGameSceen : MonoBehaviour
{
    [SerializeField] private TMP_InputField InputField;

    private string _nameCaracter;
    private Color _colorCharacter;

    public event UnityAction<string> nameAlready;
    public event UnityAction<Color> ColorAlready;

    private void Start()
    {
        Time.timeScale = 0;
    }

    private void OnEndedEdit()
    {
        _nameCaracter = InputField.text;
        nameAlready?.Invoke(_nameCaracter);
    }

    public void OnClickButton(Button item)
    {
        _colorCharacter = item.image.color;
        ColorAlready?.Invoke(_colorCharacter);
    }
}
