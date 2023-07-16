using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class EnemyCar : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Transform _car;
    [SerializeField] Vector3 _offset;

    PathFollower _racePathFollower;

    private void Awake()
    {
        _racePathFollower = GetComponent<PathFollower>();
        _racePathFollower.speed = 0;
    }

    private void Update()
    {
        if (_car == null)
        {
            _car = transform.GetChild(0).transform;
        }

        ChangePosition();  
    }

    void ChangePosition()
    {
        Vector3 pos = _car.transform.localPosition;
        pos.x = _offset.x;
        //pos.y = _offset.y;
        //pos.z = _offset.z;
        _car.transform.localPosition = pos;
    }
}
