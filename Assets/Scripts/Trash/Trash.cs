using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] private int _valueOfRewardPoints;
    [SerializeField] private int _levelTrash;

    public int RewardTrash => _valueOfRewardPoints;
    public int LevelTrash => _levelTrash;

}
