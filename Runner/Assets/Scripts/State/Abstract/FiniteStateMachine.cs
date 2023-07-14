using UnityEngine;
using System.Collections.Generic;

public abstract class FiniteStateMachine<T> : MonoBehaviour
    where T : MonoBehaviour
{
    [SerializeField] private State<T>[] _states;
    private readonly Dictionary<string, State<T>> _stateByName = new Dictionary<string, State<T>>();
    protected State<T> activeState;

    private void Awake()
    {
        foreach (State<T> state in _states)
        {
            _stateByName.Add(state.StateName, state);
        }
    }

    protected virtual State<T> InitialState()
    {
        return _states[0];
    }

    protected virtual void Start()
    {
        SetState(InitialState());
    } 

    protected virtual void FixedUpdate()
    {
        activeState.FixedUpdateState();
    }

    protected virtual void Update()
    {
        activeState.UpdateState();
    }

    public virtual void SetState(State<T> state)
    {       
        if (activeState == state) return;

        if(activeState != null) activeState.ExitState();

        activeState = state;
        state.InitState(GetComponent<T>());
        state.EnterState();
    }

    public virtual void SetState(string stateName)
    {
        SetState(GetState(stateName));
    }

    public virtual void SetState(int index)
    {
        SetState(GetState(index));
    }

    protected State<T> GetState(string stateName)
    {       
        return _stateByName[stateName];
    }

    protected State<T> GetState(int index)
    {
        return _states[index];
    }
}
