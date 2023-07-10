using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class BoosterTrigger : MonoBehaviour
{
    public static Action<bool> SpeedUp;

    private void OnTriggerEnter(Collider other)
    {
        PathFollower pathFollower = other.GetComponentInParent<PathFollower>();

        if (other.CompareTag("Player"))
        {
            StartCoroutine(ChangePlayerSpeedTemporarily(2.5f, pathFollower));
        }
    }

    IEnumerator ChangePlayerSpeedTemporarily(float time, PathFollower pathFollower)
    {
        SpeedUp?.Invoke(true);
        pathFollower.speed += 5;
        yield return new WaitForSeconds(time);
        pathFollower.speed -= 5;
        SpeedUp?.Invoke(false);
    }
}

