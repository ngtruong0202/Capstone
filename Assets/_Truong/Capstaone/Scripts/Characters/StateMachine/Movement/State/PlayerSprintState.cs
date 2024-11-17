using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Truong
{
    public class PlayerSprintState : PlayerGroundedState
    {
        private PlayerSprintData sprintData;

        private bool keepSprinting;
        private float startTime;

        public PlayerSprintState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            sprintData = movementData.SprintData;
        }

        public override void Enter()
        {
            base.Enter();


            stateMachine.ReusableData.MovementSpeedModifier = sprintData.SpeedMotifier;

            startTime = Time.time;
        }

        public override void UpDate()
        {
            base.UpDate();

            if (keepSprinting)
            {
                return;
            }

            if (Time.time < startTime + sprintData.SprintToRunTime)
            {
                return;
            }

            StopSprinting();
        }

        private void StopSprinting()
        {
            if (stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.IdlingState);
                return;
            }

            stateMachine.ChangeState(stateMachine.RunningState);
        }

        protected override void AddInputActionCallbacks()
        {
            base.AddInputActionCallbacks();

            stateMachine.Player.Inputs.playerActions.Sprint.performed += OnsprintPerformed;
        }

        protected override void RemoveInputActionCallbacks()
        {
            base.RemoveInputActionCallbacks();

            stateMachine.Player.Inputs.playerActions.Sprint.performed -= OnsprintPerformed;
        }

        private void OnsprintPerformed(InputAction.CallbackContext context)
        {
            keepSprinting = true;
        }
    }
}

