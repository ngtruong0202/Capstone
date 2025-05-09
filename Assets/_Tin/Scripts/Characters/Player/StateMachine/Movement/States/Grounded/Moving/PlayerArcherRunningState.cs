using UnityEngine.InputSystem;

namespace _Tin.Scripts.Characters.Player.StateMachine.Movement.States.Grounded.Moving
{
    public class PlayerArcherRunningState : PlayerArcherMovingState
    {
        public PlayerArcherRunningState(PlayerArcherStateMachine playerArcherStateMachine) : base(playerArcherStateMachine)
        {}

        #region ISate Methods
        public override void Enter()
        {
            base.Enter();
            ArcherStateMachine.ArcherStateReusableData.MovementSpeedModifier =
                ArcherMovementData.ArcherRunData.SpeedModifier;
        }
        #endregion
        
        #region Input Method
        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);
            ArcherStateMachine.ChangeState(ArcherStateMachine.ArcherWalkingState);
        }
        #endregion
    }
}