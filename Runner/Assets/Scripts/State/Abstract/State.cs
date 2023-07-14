using UnityEngine;

public abstract class State<T> : ScriptableObject
    where T : MonoBehaviour
{
    public string StateName;
    
    protected T ownerMachine;

    public virtual void InitState(T machine) 
    {
        ownerMachine = machine;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void ExitState();
}
