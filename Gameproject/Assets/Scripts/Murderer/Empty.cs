using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empty : State<Murderer>
{
    public Empty()
    {
        name = "Empty";
    }

    override public void Enter(Murderer murderer)
    {

    }

    override public void Execute(Murderer murderer)
    {

    }

    override public void Exit(Murderer murderer)
    {

    }
}
