using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    Animator _animator;
    string _idleParameter = "Idle";
    string _victoryParameter = "GameFinished";
    string _speedUpParameter = "SpeedUp";
    string _runningParameter = "Running";
    string _stumbleParameter = "Stumble";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Finish.StateFinished += PlayFinishAnimation;
        BoosterTrigger.SpeedUp += PlaySpeedUpAnimation;
        Obstacle.Crashed += PlayStumbleAnimation;
    }

    private void OnDisable()
    {
        Finish.StateFinished -= PlayFinishAnimation;
        BoosterTrigger.SpeedUp -= PlaySpeedUpAnimation;
        Obstacle.Crashed -= PlayStumbleAnimation;
    }

    public void PlayFinishAnimation(StateMachine.GameStates state)
    {
        if (StateMachine.Instance.GameState == StateMachine.GameStates.Merge)
        {
            _animator.SetTrigger(_idleParameter);
        }
        else if (StateMachine.Instance.GameState == StateMachine.GameStates.Race)
        {
            _animator.SetTrigger(_victoryParameter);
        }
    }

    public void PlaySpeedUpAnimation(bool speedUp)
    {
        if (speedUp)
        {
            _animator.SetTrigger(_speedUpParameter);
        }
        else
        {
            _animator.SetTrigger(_runningParameter);
        }
    }

    public void PlayStumbleAnimation()
    {
        _animator.SetTrigger(_stumbleParameter);
    }
}
