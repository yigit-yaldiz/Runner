using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class Booster : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PathFollower pathFollower = other.GetComponentInParent<PathFollower>();
        
        if (other.CompareTag("Player"))
        {

        }
    }

    IEnumerator ChangePlayerSpeedTemporarily()
    {

    }
}
