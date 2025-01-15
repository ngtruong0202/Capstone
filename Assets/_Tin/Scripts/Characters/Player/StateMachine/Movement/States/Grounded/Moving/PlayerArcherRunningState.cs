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
        
        #region Input Method
        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);
            _archerStateMachine.ChangeState(_archerStateMachine.ArcherWalkingState);
        }
        #endregion
    }
}