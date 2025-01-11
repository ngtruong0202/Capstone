using UnityEngine;
using UnityEngine.InputSystem;

namespace _Tin.Scripts.Characters.Player.StateMachine.Movement.States.Grounded.Moving
{
    public class PlayerArcherWalkingState : PlayerArcherState
    {
        public PlayerArcherWalkingState(PlayerArcherStateMachine playerArcherStateMachine) : base(playerArcherStateMachine)
        {}
        
        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Debug.Log("PlayerArcherWalkingState Enter: " + GetType().Name);
            
            SpeedModifier = 0.25f;
        }
        #endregion

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
        
        #region Input Method
        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);
            _archerStateMachine.ChangeState(_archerStateMachine.ArcherRunningState);
        }
        protected void OnMovementCanceled(InputAction.CallbackContext context)
        {
            _archerStateMachine.ChangeState(_archerStateMachine.ArcherIdlingState);
        }
        #endregion
    }
}