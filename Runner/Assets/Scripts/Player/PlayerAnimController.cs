using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

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

    public void PlayFinishAnimation(GameStates state)
    {
        if (state == GameStates.Merge - 1)
        {
            _animator.SetTrigger(_idleParameter);
        }
        else if (state == GameStates.Race)
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

    public void PlayStumbleAnimation(PathFollower pathFollower)
    {
        _animator.SetTrigger(_stumbleParameter);
    }
}
