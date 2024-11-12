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

            stateMachine.ReusableData.MovementSpeedModifier = movementData.RunData.SpeedModifier;

            StartAnimation(stateMachine.Player.PlayerAnimation.Running);
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(stateMachine.Player.PlayerAnimation.Running);

        }
    }
}

