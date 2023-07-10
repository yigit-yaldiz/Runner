using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class Obstacle : MonoBehaviour
{
    public static Action Crashed;
    public static bool IsItCrashed;

    private void OnEnable()
    {
        Crashed += KickBackPreparation;
        Crashed += () => { StartCoroutine(KickBackLerp(1f)); };
    }

    private void OnDisable()
    {
        Crashed -= KickBackPreparation;
        Crashed -= () => { StartCoroutine(KickBackLerp(1f)); };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Crashed();
        }
    }

    void KickBackPreparation()
    {
        PathFollower.Instance.speed = 0;
        IsItCrashed = true;
    }

    IEnumerator KickBackLerp(float animTime)
    {
        float elapsedTime = 0;
        float firstDistanceTravelled = PathFollower.Instance.distanceTravelled;
        float lastDistanceTravelled = firstDistanceTravelled - 2f;

        while (elapsedTime < animTime)
        {
            elapsedTime += Time.deltaTime;
            PathFollower.Instance.distanceTravelled = Mathf.Lerp(firstDistanceTravelled, lastDistanceTravelled, elapsedTime / animTime);
            yield return null;
        }

        PathFollower.Instance.distanceTravelled = lastDistanceTravelled;

        IsItCrashed = false;
    }
}
