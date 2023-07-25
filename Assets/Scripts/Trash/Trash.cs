using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] private int _valueOfRewardPoints;

    public int RewardTrash => _valueOfRewardPoints;
}
