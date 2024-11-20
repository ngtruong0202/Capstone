using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Truong
{
    public class PlayerGroundedState : PlayerMovementState
    {
        public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        protected override void AddInputActionCallbacks()
        {          
            stateMachine.Player.Inputs.playerActions.Movement.canceled += OnMovementCanceled;
            stateMachine.Player.Inputs.playerActions.Jump.started += OnJumpStarted;
            stateMachine.Player.Inputs.playerActions.Dash.started += OnDashStarted;   
        }

        

        protected override void RemoveInputActionCallbacks()
        {
            stateMachine.Player.Inputs.playerActions.Movement.canceled -= OnMovementCanceled;
            stateMachine.Player.Inputs.playerActions.Jump.started -= OnJumpStarted;
            stateMachine.Player.Inputs.playerActions.Dash.started -= OnDashStarted;
        }
        protected void OnMove()
        {
            stateMachine.ChangeState(stateMachine.RunningState);
        }

        protected void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
        }

        protected virtual void OnDashStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.DashingState);
        }

        protected virtual void OnJumpStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.JumpingState);
        }

        public void StopAgent()
        {
            if(stateMachine.Player.Agent.enabled)
            {
                stateMachine.Player.Agent.isStopped = true;
                stateMachine.Player.Agent.enabled = false;

            }
        }
    }

}
