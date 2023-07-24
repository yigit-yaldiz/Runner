using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceStarter : MonoBehaviour
{
    public static Action<GameStates> RaceStarted;

    private void OnEnable()
    {
        Finish.StateFinished += StartTheRace;
    }

    private void OnDisable()
    {
        Finish.StateFinished -= StartTheRace;
    }

    void StartTheRace(GameStates state)
    {
        if (state != GameStates.Merge)
        {
            return;
        }

        StartCoroutine(Countdown(3));

        IEnumerator Countdown(int seconds)
        {
            int counter = seconds;
            
            while (counter > 0)
            {
                yield return new WaitForSeconds(1);
                counter--;

                UIElementsController.Instance.CountdownText.text = counter.ToString();
            }

            UIElementsController.Instance.CountdownText.gameObject.SetActive(false);

            RaceStarted(state);
        }
    }
}
