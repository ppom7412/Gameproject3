using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<T> {
    public string name;
    public State() {
        name = "State";
    }
    virtual public void Enter(T machine) { }
    virtual public void Execute(T machine) { }
    virtual public void Exit(T machine) { }
}
