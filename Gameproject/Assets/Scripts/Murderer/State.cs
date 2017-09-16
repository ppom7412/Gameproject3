using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State {
    public string name;
    public State() {
        name = "State";
    }
    virtual public void Enter(Murderer murderer) { }
    virtual public void Execute(Murderer murderer) { }
    virtual public void Exit(Murderer murderer) { }
}
