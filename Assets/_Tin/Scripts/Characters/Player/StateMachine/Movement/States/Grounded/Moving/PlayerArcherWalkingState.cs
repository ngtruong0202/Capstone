using UnityEngine;
using UnityEngine.InputSystem;

namespace _Tin.Scripts.Characters.Player.StateMachine.Movement.States.Grounded.Moving
{
    public class PlayerArcherWalkingState : PlayerArcherGroundedState
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

        #region Input Method
        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);
            _archerStateMachine.ChangeState(_archerStateMachine.ArcherRunningState);
        }
        
        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            base.OnMovementCanceled(context);
        }

        #endregion
    }
}