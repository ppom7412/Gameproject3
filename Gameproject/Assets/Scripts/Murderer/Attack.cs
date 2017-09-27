using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State<Murderer>
{
    private float time = 0.0f;

    public Attack()
    {
        name = "Attack";
    }

    override public void Enter(Murderer murderer)  {
        Debug.Log("In Attack");
        murderer.animator.SetBool("isAttack", true);
    }

    override public void Execute(Murderer murderer) {
        if (time == 0.0f)
        {
            murderer.Attacking();
            time += Time.deltaTime;
        }
        else if (time > murderer.GetAttackDelay())
        {
            time = 0;
        }
        else {
            time += Time.deltaTime;
        }
    }

    override public void Exit(Murderer murderer) {
        Debug.Log("Out Attack");
        murderer.animator.SetBool("isAttack", false);
    }
}

