using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardViewRang : MonoBehaviour
{
[SerializeField] private TMP_Text _playerName;
[SerializeField] private TMP_Text _playerPoint;
[SerializeField] private TMP_Text _playerPosition;

public void RenderView(int position, string playerName, int point)
{
    _playerName.text = playerName;
    _playerPoint.text = point.ToString();
    _playerPosition.text = position.ToString();
}
}
