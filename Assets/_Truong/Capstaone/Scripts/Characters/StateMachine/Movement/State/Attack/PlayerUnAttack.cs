using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Truong
{
    public class PlayerUnAttack : PlayerEventAttactState
    {
        public PlayerUnAttack(PlayerAttackStateMachine playerAttackState) : base(playerAttackState)
        {
        }

        public override void Enter()
        {
            base.Enter();

            stateMachine.Player.Inputs.playerActions.Movement.Enable();

           
        }


    }

}
