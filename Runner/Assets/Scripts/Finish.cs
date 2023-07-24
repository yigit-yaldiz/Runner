using System;
using UnityEngine;
using Cinemachine;

public class Finish : MonoBehaviour
{
    public static bool IsRunnerFinished;
    public static bool IsRaceFinished;
    public static Action<GameStates> StateFinished;

    [SerializeField] Cinemachine.CinemachineVirtualCamera _raceCam;

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
        GameStates state = StateMachine.Instance.GameState;

        if (other.CompareTag("Player"))
        {
            StateFinished(state);

            if (state == GameStates.Runner)
            {
                IsRunnerFinished = true;
            }
            else if (state == GameStates.Race)
            {
                Debug.Log("You won");
                IsRaceFinished = true;
            }
        }
    }

    void GoToNextState(GameStates state)
    {
        StateMachine.Instance.SetTheState(state + 1);
    }

    public void FollowTheMergedVehicle(GameObject car)
    {
        _raceCam.Follow = car.transform;
        _raceCam.LookAt = car.transform;
    }

    public void OnRaceButtonClick()
    {
        GameStates state = StateMachine.Instance.GameState;
        FollowTheMergedVehicle(CarSelecting.Instance.MergedVehicle);
        UIElementsController.Instance.CountdownText.gameObject.SetActive(true);
        StateFinished(state);
    }
}
