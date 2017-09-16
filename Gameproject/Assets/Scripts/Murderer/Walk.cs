using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : State {
    private float time = 0.0f;
    private float ramdomTime;

    public Walk()
    {
        ramdomTime = Random.Range(1.0f, 5.0f);
        name = "Walk";
    }

    override public void Enter(Murderer murderer) {
        Debug.Log("In Walk");
    }

    override public void Execute(Murderer murderer) {
        murderer.Walking();
        time += Time.deltaTime;

        if (time > ramdomTime)
            murderer.murdererMachine.ChangeState(new Wait());
    }

    override public void Exit(Murderer murderer) {
        Debug.Log("Out Walk");
    }
}
