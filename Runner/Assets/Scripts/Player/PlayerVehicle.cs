using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;

public class PlayerVehicle : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Transform _car;

    [SerializeField] Vector3 _offset;

    PathFollower _racePathFollower;

    private void Awake()
    {
        _racePathFollower = GetComponent<PathFollower>();
    }
    private void OnEnable()
    {
        RaceStarter.RaceStarted += LetsRace;
    }

    private void OnDisable()
    {
        RaceStarter.RaceStarted -= LetsRace;
    }

    private void Start()
    {
        if (_car == null)
        {
            _car = transform.GetChild(0).transform;
        }

        _racePathFollower.speed = 0;
    }

    void ChangePosition()
    {
        Vector3 pos = _car.transform.localPosition;
        pos.x = _offset.x;
        //pos.y = _offset.y;
        //pos.z = _offset.z;
        _car.transform.localPosition = pos;
    }

    void LetsRace(GameStates gameState)
    {
        ChangePosition();

        if (gameState == GameStates.Merge)
        {
            _racePathFollower.speed = _speed;
        }
    }

    public void SetRacePathToVehicle()
    {
        _racePathFollower.pathCreator = RacePath.Instance.PathCreator;
    }
}
