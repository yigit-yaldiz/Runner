using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class PlayerSpeedController : MonoBehaviour
{
    [SerializeField] PathFollower _pathFollower;
    float _defaultSpeed;

    private void Awake()
    {
        _defaultSpeed = _pathFollower.speed;
    }

    public void ReturnToDefaultSpeed()
    {
        _pathFollower.speed = _defaultSpeed;
        Obstacle.IsItCrashed = false;
    }
}
