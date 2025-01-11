using UnityEngine;

namespace _Tin.Scripts.Characters.Player.StateMachine.Movement.States.Grounded
{
    public class PlayerArcherIdlingState : PlayerArcherState
    {
        public PlayerArcherIdlingState(PlayerArcherStateMachine playerArcherStateMachine) : base(playerArcherStateMachine)
        {}

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Debug.Log("PlayerArcherIdlingState Enter: " + GetType().Name);
            
            SpeedModifier = 0f;
            ResetVelocity();
        }

        public override void Update()
        {
            base.Update();
            if(MovementInput == Vector2.zero)
                return;

            AddForceToPlayer();
        }

        #endregion
    }
}