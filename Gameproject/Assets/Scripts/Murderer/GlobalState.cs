using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//상태변이조건 걸어주는 클래스
public class GlobalState : State {
    override public void Enter(Murderer murderer) { }
    override public void Exit(Murderer murderer) { }

    override public void Execute(Murderer murderer) {

        /// 1. 공격범위 검사
        if (murderer.CheckAttackArea())
        {
            if (murderer.murdererMachine.GetCurrentStateName() != "Attack") {
                murderer.murdererMachine.ChangeState(new Attack());
                return;
            }
        }

        /// 2. 추격범위 검사
        else if (murderer.CheckSensitiveArea())
        {
            if (murderer.murdererMachine.GetCurrentStateName() != "Chase"){
                murderer.murdererMachine.ChangeState(new Chase());
                return;
            }
        }

        /// 3. 대기상태로 돌리기.
        else
        {
            if (murderer.murdererMachine.GetCurrentStateName() == "Wait"
                || murderer.murdererMachine.GetCurrentStateName() == "Walk")
            {
            }

            else {
                murderer.murdererMachine.ChangeState(new Wait());
            }

        }

    }
}
