using System;
using System.Collections;
using UnityEngine;
using PathCreation.Examples;

public class Obstacle : MonoBehaviour
{
    public static Action<PathFollower> Crashed;
    public static bool IsItCrashed;

    private void OnEnable()
    {
        Crashed += KickBackPreparation;
        Crashed += KickBack;
        //Crashed += () => { StartCoroutine(KickBackLerp(1f)); };
    }

    private void OnDisable()
    {
        Crashed -= KickBackPreparation;
        Crashed -= KickBack;
        //Crashed -= () => { StartCoroutine(KickBackLerp(1f)); };
    }

    private void OnTriggerEnter(Collider other)
    {
        PathFollower _pathFollower = other.GetComponentInParent<PathFollower>();

        if (other.gameObject.CompareTag("Player"))
        {
            Crashed(_pathFollower);
        }
    }

    void KickBackPreparation(PathFollower pathFollower)
    {
        IsItCrashed = true;
        pathFollower.speed = 0;
    }


    void KickBack(PathFollower pathFollower)
    {
        StartCoroutine(KickBackLerp(1f));

        IEnumerator KickBackLerp(float animTime)
        {
            float elapsedTime = 0;
            float firstDistanceTravelled = pathFollower.distanceTravelled;
            float lastDistanceTravelled = firstDistanceTravelled - 2f;

            while (elapsedTime < animTime)
            {
                elapsedTime += Time.deltaTime;
                pathFollower.distanceTravelled = Mathf.Lerp(firstDistanceTravelled, lastDistanceTravelled, elapsedTime / animTime);
                yield return null;
            }

            pathFollower.distanceTravelled = lastDistanceTravelled;
        }
    }
}
