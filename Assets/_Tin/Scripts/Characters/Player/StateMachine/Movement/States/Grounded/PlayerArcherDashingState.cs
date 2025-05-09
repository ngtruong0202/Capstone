using _Tin.Scripts.Characters.Player.Data.States.Grounded.Moving;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Tin.Scripts.Characters.Player.StateMachine.Movement.States.Grounded
{
    public class PlayerArcherDashingState : PlayerArcherGroundedState
    {
        private readonly PlayerArcherDashData _archerDashData;
        private float _startTime;
        private int _consecutiveDashesUsed;
        public PlayerArcherDashingState(PlayerArcherStateMachine archerStateMachine) : base(archerStateMachine) => 
            _archerDashData = ArcherMovementData.ArcherDashData;
        
        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            ArcherStateMachine.ArcherStateReusableData.MovementSpeedModifier = ArcherMovementData.ArcherDashData.SpeedModifier;
            AddForceOnTransitionFromStationaryState();
            UpdateConsecutiveDashesUsed();
            _startTime = Time.time;
        }

        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();

            if (ArcherStateMachine.ArcherStateReusableData.MovementInput == Vector2.zero)
            {
                ArcherStateMachine.ChangeState(ArcherStateMachine.ArcherIdlingState);
                return;
            }
            ArcherStateMachine.ChangeState(ArcherStateMachine.ArcherSprintingState);
        }

        #endregion

        #region Main Methods
        private void AddForceOnTransitionFromStationaryState()
        {
            if(ArcherStateMachine.ArcherStateReusableData.MovementInput != Vector2.zero)
                return;
            
            Vector3 characterRotationDirection = ArcherStateMachine.PlayerArcher.MainCameraTransform.forward;
            characterRotationDirection.y = 0f;
            
            ArcherStateMachine.PlayerArcher.Rigidbody.velocity = characterRotationDirection * GetMovementSpeed();
        }
        
        private void UpdateConsecutiveDashesUsed()
        {
            if (!IsConsecutive()) 
                _consecutiveDashesUsed = 0;
            ++_consecutiveDashesUsed;
            
            if(_consecutiveDashesUsed == _archerDashData.ConsecutiveDashesLimitAmount)
            {
                _consecutiveDashesUsed = 0;
                ArcherStateMachine.PlayerArcher.ArcherInput.DisableActionFor(
                    ArcherStateMachine.PlayerArcher.ArcherInput.PlayerArcherActions.Dash,
                    _archerDashData.DashLimitReachedCooldown);
            }
        }
        
        private bool IsConsecutive() => 
            Time.time < _startTime + _archerDashData.TimeToBeConsideredConsecutive;
        #endregion

        #region Input Methods

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            base.OnMovementCanceled(context);
        }

        protected override void OnDashStarted(InputAction.CallbackContext context)
        {
            base.OnDashStarted(context);
        }

        #endregion
    }
}