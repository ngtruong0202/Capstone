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
            _archerStateMachine.PlayerArcher.ArcherInput.PlayerArcherActions.Movement.canceled += OnMovementCanceled;
        }
        protected override void RemoveInputActionCallbacks()
        {
            base.RemoveInputActionCallbacks();
            _archerStateMachine.PlayerArcher.ArcherInput.PlayerArcherActions.Movement.canceled -= OnMovementCanceled;
        }
        
        protected virtual void OnAddForceToPlayer()
        {
            if(shouldWalk)
            {
                _archerStateMachine.ChangeState(_archerStateMachine.ArcherWalkingState);
                return;
            }
            
            _archerStateMachine.ChangeState(_archerStateMachine.ArcherRunningState);
        }
        #endregion
        
        #region Input Method
        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);
            _archerStateMachine.ChangeState(_archerStateMachine.ArcherRunningState);
        }

        protected virtual void OnMovementCanceled(InputAction.CallbackContext context) => 
            _archerStateMachine.ChangeState(_archerStateMachine.ArcherIdlingState);
        #endregion
    }
}