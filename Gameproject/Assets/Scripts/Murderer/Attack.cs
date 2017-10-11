using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State<Murderer>
{
    public Attack()
    {
        name = "Attack";
    }

    override public void Enter(Murderer murderer){
        Debug.Log("In Attack");
        murderer.StartCoroutine(murderer.StartAttack());
    }

    override public void Execute(Murderer murderer) {

    }

    override public void Exit(Murderer murderer) {
        Debug.Log("Out Attack");
        murderer.StopAllCoroutines();
        //murderer.StopCoroutine(murderer.StartAttack());
        murderer.animator.SetBool("isAttack", false);
    }
}

