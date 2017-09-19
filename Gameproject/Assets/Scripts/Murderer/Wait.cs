using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait : State<Murderer>
{
    private float time = 0.0f;
    private float ramdomTime;

    public Wait()
    {
        ramdomTime = Random.Range(1.0f, 5.0f);
        name = "Wait";
    }

    override public void Enter(Murderer murderer){
        Debug.Log("In Wait");
    }

    override public void Execute(Murderer murderer) {
        murderer.Waiting();
        time += Time.deltaTime;

        if (time > ramdomTime)
            murderer.murdererMachine.ChangeState(new Walk());
    }

    override public void Exit(Murderer murderer) {
        Debug.Log("Out Wait");
    }
}
