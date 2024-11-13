using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Truong
{
    public class PlayerDashingState : PlayerGroundedState
    {
        private PlayerDashData dashData;
        private float startTime;

        private int consecutiveDashesUsed;

        public PlayerDashingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            dashData = movementData.DashData;
        }

        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableData.MovementSpeedModifier = movementData.DashData.SpeedModifier;

            AddForceOnTransitionFromStationaryState();

            UpdateConsecutiveDashes();

            startTime = Time.time;

        }

        public override void OnAnimationEnterEvent()
        {
            base.OnAnimationEnterEvent();

            if (stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.IdlingState);
                return;
            }

            stateMachine.ChangeState(stateMachine.SprintingState);
        }

        protected override void OnDashStarted(InputAction.CallbackContext context)
        {

        }

        private void UpdateConsecutiveDashes()
        {
            if (Consecutive())
            {
                consecutiveDashesUsed = 0;
            }

            consecutiveDashesUsed++;

            if (consecutiveDashesUsed == dashData.ConsecutiveDashesLimitAmount)
            {
                consecutiveDashesUsed = 0;

                stateMachine.Player.Inputs.DisableActionFor(stateMachine.Player.Inputs.playerActions.Dash, dashData.DashLimitReachedCooldown);
            }

        }
        private bool Consecutive()
        {
            return Time.time < startTime + dashData.TimeToBeConsideredConsecutive;
        }

        private void AddForceOnTransitionFromStationaryState()
        {
            if (stateMachine.ReusableData.MovementInput != Vector2.zero)
            {
                return;
            }

            Vector3 characterRotationDirection = stateMachine.Player.transform.forward;
            characterRotationDirection.y = 0f;

            stateMachine.Player.Rigidbody.velocity = characterRotationDirection * GetMovementSpeed();
        }
    }
}

