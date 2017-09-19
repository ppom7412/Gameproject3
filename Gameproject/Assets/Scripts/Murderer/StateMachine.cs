using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> {
    private T owner;

    private State<T> globalState;
    private State<T> previousState;
    private State<T> currentState;

    public StateMachine(GameObject _owner) 
    {
        if (_owner.GetComponent<T>() == null)
            ErrorAdmin.ErrorMessege("_owenr var is null","StateMachine()");
            
        owner = _owner.GetComponent<T>();

        globalState = null;
        previousState = null;
        currentState = null;
    }

    public void ChangeState(State<T> _state){
        if (_state == null){
            ErrorAdmin.ErrorMessege("_state var is missing", "ChangeState()");
            return;
        }

        previousState = currentState;
        currentState.Exit(owner);
        currentState = _state;
        currentState.Enter(owner);
    }

    public void SetPreviousState(State<T> _state)
    {
        if (_state == null)
        {
            ErrorAdmin.ErrorMessege("_state var is missing", "SetPreviousState()");
            return;
        }

        previousState = _state;
    }

    public void SetGlobalState(State<T> _state)
    {
        if (_state == null)
        {
            ErrorAdmin.ErrorMessege("_state var is missing", "SetGlobalState()");
            return;
        }

        globalState = _state;
    }

    public void SetCurrentState(State<T> _state)
    {
        if (_state == null)
        {
            ErrorAdmin.ErrorMessege("_state var is missing", "SetCurrentState()");
            return;
        }

        currentState = _state;
    }

    public State<T> GetPreviousState()
    {
        return previousState;
    }

    public State<T> GetGlobalState()
    {
        return globalState;
    }

    public State<T> GetCurrentState()
    {
        return currentState;
    }

    public void Update(){
        if (owner == null)
        {
            ErrorAdmin.ErrorMessege("owner is null", "Update()");
            return;
        }

        if (globalState != null) globalState.Execute(owner);
        if (currentState != null) currentState.Execute(owner);
    }

    public void RevertToPreviousState(){
        ChangeState(previousState);
    }

    public string GetCurrentStateName() {
        return currentState.name;
    }
}
