using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class PlayerSpeedController : MonoBehaviour
{
    public PathFollower PathFollower => _pathFollower;

    PathFollower _pathFollower;
    float _defaultSpeed;

    private void Awake()
    {
        _pathFollower = GetComponentInParent<PathFollower>();
    }

    private void Start()
    {
        _defaultSpeed = _pathFollower.speed;
    }

    public void ReturnToDefaultSpeed()
    {
        _pathFollower.speed = _defaultSpeed;
        Obstacle.IsItCrashed = false;
    }
}
