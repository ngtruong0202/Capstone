using UnityEngine;
using UnityEngine.InputSystem;

namespace _Tin.Scripts.Characters.Player.StateMachine.Movement.States.Grounded
{
    public class PlayerArcherGroundedState : PlayerArcherState
    {
        private readonly SlopeData _slopeData;
        protected PlayerArcherGroundedState(PlayerArcherStateMachine archerStateMachine) : base(archerStateMachine) => 
            _slopeData = ArcherStateMachine.PlayerArcher.ArcherCapsuleColliderUtility.SlopeData;

        #region IState Methods

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            Float();
        }
        
        #endregion

        #region Main Methods

        private void Float()
        {
            Vector3 capsuleColliderCenterInWorldSpace = ArcherStateMachine.PlayerArcher.ArcherCapsuleColliderUtility
                .CapsuleColliderData.Collider.bounds.center;
            Ray downwardRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);
            
            if(Physics.Raycast(downwardRayFromCapsuleCenter, out RaycastHit hit, _slopeData.FloatRayDistance, ArcherStateMachine.PlayerArcher.ArcherLayerData.GroundLayer, QueryTriggerInteraction.Ignore))
            {
                float groundAngle = Vector3.Angle(hit.normal, -downwardRayFromCapsuleCenter.direction);
                float slopeSpeedModifier = SetSlopeSpeedModifierOnAngle(groundAngle);
                
                if(slopeSpeedModifier == 0f) return;
                
                float distanceToFloatingPoint = ArcherStateMachine.PlayerArcher.ArcherCapsuleColliderUtility.CapsuleColliderData.ColliderCenterInLocalSpace.y * ArcherStateMachine.PlayerArcher.transform.localScale.y - hit.distance;
                
                if(distanceToFloatingPoint == 0f) return;
                
                float amountToLift = distanceToFloatingPoint * _slopeData.StepReachForce - GetPlayerArcherVerticalVelocity().y;
                Vector3 liftForce = new Vector3(0f, amountToLift, 0f);
                ArcherStateMachine.PlayerArcher.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
            }
        }
        
        private float SetSlopeSpeedModifierOnAngle(float angle)
        {
            float slopeSpeedModifier = ArcherMovementData.SlopeSpeedAngles.Evaluate(angle);
            ArcherStateMachine.ArcherStateReusableData.MovementOnSlopesSpeedModifier = slopeSpeedModifier;
            
            return slopeSpeedModifier;
        }
        
        #endregion
        
        #region Reusable Methods
        protected override void AddInputActionCallbacks()
        {
            base.AddInputActionCallbacks();
            ArcherStateMachine.PlayerArcher.ArcherInput.PlayerArcherActions.Movement.canceled += OnMovementCanceled;
            ArcherStateMachine.PlayerArcher.ArcherInput.PlayerArcherActions.Dash.started += OnDashStarted;
        }
        protected override void RemoveInputActionCallbacks()
        {
            base.RemoveInputActionCallbacks();
            ArcherStateMachine.PlayerArcher.ArcherInput.PlayerArcherActions.Movement.canceled -= OnMovementCanceled;
            ArcherStateMachine.PlayerArcher.ArcherInput.PlayerArcherActions.Dash.started -= OnDashStarted;
        }
        
        protected virtual void OnAddForceToPlayer()
        {
            if(ArcherStateMachine.ArcherStateReusableData.ShouldWalk)
            {
                ArcherStateMachine.ChangeState(ArcherStateMachine.ArcherWalkingState);
                return;
            }
            
            ArcherStateMachine.ChangeState(ArcherStateMachine.ArcherRunningState);
        }
        #endregion
        
        #region Input Method
        protected virtual void OnDashStarted(InputAction.CallbackContext context) => 
            ArcherStateMachine.ChangeState(ArcherStateMachine.ArcherDashingState);

        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);
            ArcherStateMachine.ChangeState(ArcherStateMachine.ArcherRunningState);
        }

        protected virtual void OnMovementCanceled(InputAction.CallbackContext context) => 
            ArcherStateMachine.ChangeState(ArcherStateMachine.ArcherIdlingState);
        #endregion
    }
}