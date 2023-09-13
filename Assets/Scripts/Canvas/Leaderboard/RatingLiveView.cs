using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RatingLiveView : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerPositionText;
    [SerializeField] private TMP_Text _playerNameText;
    [SerializeField] private TMP_Text _playerPointText;

    private Animator _textAnimator;

    public string NameRating => _playerNameText.text;

    private void Start()
    {
        _textAnimator = GetComponent<Animator>();
        _textAnimator.GetBool("IsTakedPoint");
    }

    public void AnimateText()
    {
        _textAnimator.SetBool("IsTakedPoint", true);
    }

    public void EndAnimationTextRating()
    {
        _textAnimator.SetBool("IsTakedPoint", false);
    }

    public void RenderLiveView(VacuumCleaner player, string position)
    {
        _playerPositionText.text = position;
        _playerNameText.text = player.NamePlayer;
        _playerPointText.text = player.TotalExperienceForLiderboard.ToString();
    }
}
