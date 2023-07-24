using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class BoosterTrigger : MonoBehaviour
{
    public static Action<bool> SpeedUp;
    public static Action ReturnNormalSpeed;

    private void OnTriggerEnter(Collider other)
    {
        PathFollower pathFollower = other.GetComponentInParent<PathFollower>();

        if (other.CompareTag("Player"))
        {
            StartCoroutine(ChangePlayerSpeedTemporarily(2.5f, 5f, pathFollower));
        }
    }

    IEnumerator ChangePlayerSpeedTemporarily(float time, float value, PathFollower pathFollower)
    {
        SpeedUp?.Invoke(true);
        pathFollower.speed += value;
        yield return new WaitForSeconds(time);

        if (pathFollower.speed > 0)
        {
            pathFollower.speed -= value;
        }
        else
        {
            ReturnNormalSpeed?.Invoke();
        }

        SpeedUp?.Invoke(false);
    }
}

