using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] private int _valueOfRewardPoints;
  
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            player.ChangeExperienceCount(_valueOfRewardPoints);
            Destroy(gameObject);
        }
    }
}
