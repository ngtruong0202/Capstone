using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Truong
{
    public class PlayerBasicAttackState : PlayerEventAttactState
    {
        public PlayerBasicAttackState(PlayerAttackStateMachine playerAttackState) : base(playerAttackState)
        {
        }


        public override void Enter()
        {
            base.Enter();

            //stateMachine.Player.Rigidbody.velocity = Vector3.zero;

            ////stateMachine.Player.Inputs.playerActions.Movement.Disable();
            //stateMachine.Player.movementStateMachine.ChangeState(stateMachine.Player.movementStateMachine.IdlingState);


            BasicAttacks();

            startTime = Time.time;

        }

        public override void Exit()
        {
            base.Exit();

            //stateMachine.Player.Inputs.playerActions.Movement.Enable();
        }

        
    }
}

