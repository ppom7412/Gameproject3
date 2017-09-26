using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State<Murderer>
{
    public Chase()
    {
        name = "Chase";
    }

    override public void Enter(Murderer murderer) {
        Debug.Log("In Chase");
        murderer.animator.SetBool("isChase", true);
        murderer.ResetWaypoint();
        murderer.agent.Resume();
    }

    override public void Execute(Murderer murderer){
        //너무 가까우면 멈춘다.
        if (murderer.CheckMinDistanse())
        {
            murderer.ResetSoundSpot();
        }
    }

    override public void Exit(Murderer murderer) {
        Debug.Log("Out Chase");
        murderer.animator.SetBool("isChase", false);

        murderer.agent.Stop();
    }
}
