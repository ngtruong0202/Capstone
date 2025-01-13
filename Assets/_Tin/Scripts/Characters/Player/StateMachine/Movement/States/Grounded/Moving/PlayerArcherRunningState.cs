using UnityEngine.InputSystem;

namespace _Tin.Scripts.Characters.Player.StateMachine.Movement.States.Grounded.Moving
{
    public class PlayerArcherRunningState : PlayerArcherGroundedState
    {
        public PlayerArcherRunningState(PlayerArcherStateMachine playerArcherStateMachine) : base(playerArcherStateMachine)
        {}

        #region ISate Methods
        public override void Enter()
        {
            base.Enter();
            SpeedModifier = 1f;
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
            _archerStateMachine.ChangeState(_archerStateMachine.ArcherWalkingState);
        }
        protected void OnMovementCanceled(InputAction.CallbackContext context)
        {
            _archerStateMachine.ChangeState(_archerStateMachine.ArcherIdlingState);
        }
        #endregion
    }
}