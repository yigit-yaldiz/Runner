using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    Animator _animator;
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
        Finish.GameFinished += PlayFinishAnimation;
        BoosterTrigger.SpeedUp += PlaySpeedUpAnimation;
        Obstacle.Crashed += PlayStumbleAnimation;
    }

    private void OnDisable()
    {
        Finish.GameFinished -= PlayFinishAnimation;
        BoosterTrigger.SpeedUp -= PlaySpeedUpAnimation;
        Obstacle.Crashed -= PlayStumbleAnimation;
    }

    public void PlayFinishAnimation()
    {
        _animator.SetTrigger(_victoryParameter);
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
