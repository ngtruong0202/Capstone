using UnityEngine.InputSystem;

namespace _Tin.Scripts.Characters.Player.StateMachine.Movement.States.Grounded
{
    public class PlayerArcherGroundedState : PlayerArcherState
    {
        protected PlayerArcherGroundedState(PlayerArcherStateMachine archerStateMachine) : base(archerStateMachine)
        {
        }
        
        #region Reusable Methods
        protected override void AddInputActionCallbacks()
        {
            base.AddInputActionCallbacks();
            ArcherStateMachine.PlayerArcher.ArcherInput.PlayerArcherActions.Movement.canceled += OnMovementCanceled;
        }
        protected override void RemoveInputActionCallbacks()
        {
            base.RemoveInputActionCallbacks();
            ArcherStateMachine.PlayerArcher.ArcherInput.PlayerArcherActions.Movement.canceled -= OnMovementCanceled;
        }
        
        protected virtual void OnAddForceToPlayer()
        {
            if(ArcherStateMachine.ArcherStateReusableData.ShouldWalk)
            {
                ArcherStateMachine.ChangeState(ArcherStateMachine.ArcherWalkingState);
                return;
            }
            
            ArcherStateMachine.ChangeState(ArcherStateMachine.ArcherRunningState);
        }
        #endregion
        
        #region Input Method
        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);
            ArcherStateMachine.ChangeState(ArcherStateMachine.ArcherRunningState);
        }

        protected virtual void OnMovementCanceled(InputAction.CallbackContext context) => 
            ArcherStateMachine.ChangeState(ArcherStateMachine.ArcherIdlingState);
        #endregion
    }
}