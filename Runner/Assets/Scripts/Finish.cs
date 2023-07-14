using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class Finish : MonoBehaviour
{
    public static bool IsGameFinished;
    public static Action<StateMachine.GameStates> StateFinished;

    private void OnEnable()
    {
        StateFinished += GoToNextState;
    }

    private void OnDisable()
    {
        StateFinished -= GoToNextState;
    }

    private void OnTriggerEnter(Collider other)
    {
        StateMachine.GameStates state = StateMachine.Instance.GameState;

        if (other.CompareTag("Player"))
        {
            StateFinished(state);
            IsGameFinished = true;
        }
    }

    void GoToNextState(StateMachine.GameStates state)
    {
        StateMachine.Instance.SetTheState(state + 1);
    }
}
