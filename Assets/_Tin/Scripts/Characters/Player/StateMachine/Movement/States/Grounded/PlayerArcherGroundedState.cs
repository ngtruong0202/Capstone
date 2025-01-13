using UnityEngine;

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
        #endregion
    }
}