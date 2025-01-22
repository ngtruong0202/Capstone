using UnityEngine;

namespace _Tin.Scripts.Characters.Player.StateMachine.Movement.States.Grounded
{
    public class PlayerArcherIdlingState : PlayerArcherGroundedState
    {
        public PlayerArcherIdlingState(PlayerArcherStateMachine playerArcherStateMachine) : base(playerArcherStateMachine)
        {}

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Debug.Log("PlayerArcherIdlingState Enter: " + GetType().Name);
            
            ArcherStateMachine.ArcherStateReusableData.MovementSpeedModifier = 0f;
            ResetVelocity();
        }

        public override void Update()
        {
            base.Update();
            if(ArcherStateMachine.ArcherStateReusableData.MovementInput == Vector2.zero)
                return;

            OnAddForceToPlayer();
        }
        #endregion
    }
}