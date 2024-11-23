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
       
            ResetAttackState();
            BasicAttacks();

            startTime = Time.time;

        }

        public override void Exit()
        {
            base.Exit();            
        }

        
    }
}

