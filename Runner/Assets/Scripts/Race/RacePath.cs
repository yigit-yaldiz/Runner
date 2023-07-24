using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class RacePath : MonoBehaviour
{
    //this class is created because for reach the race path creator component
    public PathCreator PathCreator => _pathCreator;

    PathCreator _pathCreator;

    public static RacePath Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        _pathCreator = GetComponent<PathCreator>();
    }
}
