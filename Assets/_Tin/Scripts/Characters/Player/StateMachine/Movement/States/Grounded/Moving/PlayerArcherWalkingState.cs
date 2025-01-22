using UnityEngine;
using UnityEngine.InputSystem;

namespace _Tin.Scripts.Characters.Player.StateMachine.Movement.States.Grounded.Moving
{
    public class PlayerArcherWalkingState : PlayerArcherMovingState
    {
        public PlayerArcherWalkingState(PlayerArcherStateMachine playerArcherStateMachine) : base(playerArcherStateMachine)
        {}
        
        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Debug.Log("PlayerArcherWalkingState Enter: " + GetType().Name);
            
            ArcherStateMachine.ArcherStateReusableData.MovementSpeedModifier =
                ArcherMovementData.ArcherWalkData.SpeedModifier;
        }
        #endregion

        #region Input Method
        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);
            ArcherStateMachine.ChangeState(ArcherStateMachine.ArcherRunningState);
        }
        
        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            base.OnMovementCanceled(context);
        }

        #endregion
    }
}