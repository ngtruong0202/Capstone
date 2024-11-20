using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Truong
{
    public class PlayerRunningState : PlayerGroundedState
    {
        public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            StopAgent();

            StartAnimation(stateMachine.Player.PlayerAnimation.Running);

            stateMachine.ReusableData.MovementSpeedModifier = movementData.RunData.SpeedModifier;    
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.PlayerAnimation.Running);
        }
    }
}

