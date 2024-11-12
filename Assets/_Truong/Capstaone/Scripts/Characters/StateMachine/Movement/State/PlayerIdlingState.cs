using UnityEngine;

namespace Truong
{
    public class PlayerIdlingState : PlayerGroundedState
    {
        public PlayerIdlingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableData.MovementSpeedModifier = 0f;
            ResetVelocity();
        }

        public override void UpDate()
        {
            base.UpDate();

            if (stateMachine.ReusableData.MovementInput == Vector2.zero) return;

            OnMove();
        }
    }
}

