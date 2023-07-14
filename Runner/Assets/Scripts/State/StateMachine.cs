using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum GameStates
    {
        Runner,
        Merge,
        Race
    }

    public static StateMachine Instance { get; private set; }
    public GameStates GameState => _gameState; 

    GameStates _gameState;

    private void Awake()
    {
        Instance = this; 
    }

    private void Start()
    {
        _gameState = GameStates.Runner;
    }

    public void SetTheState(GameStates state)
    {
        _gameState = state;
    }
}
