using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine {
    private Murderer murderer;

    private State globalState;
    private State previousState;
    private State currentState;

    public StateMachine(GameObject _murderer){
        if (_murderer.GetComponent<Murderer>() != null)
            murderer = _murderer.GetComponent<Murderer>();

        globalState = new GlobalState();
        previousState = new Wait();
        currentState = new Wait();

        currentState.Enter(murderer);
    }

    public void ChangeState(State _state){
        if (_state == null)
            return;

        previousState = currentState;
        currentState.Exit(murderer);
        currentState = _state;
        currentState.Enter(murderer);
    }

    public void Update(){
        if (murderer == null) return;

        if (globalState != null) globalState.Execute(murderer);
        if (currentState != null) currentState.Execute(murderer);
    }

    public void RevertToPreviousState(){
        ChangeState(previousState);
    }

    public string GetCurrentStateName() {
        return currentState.name;
    }
}
