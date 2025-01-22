using _Tin.Scripts.Characters.Player.Data.States.Grounded;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Tin.Scripts.Characters.Player.StateMachine.Movement.States
{
    public class PlayerArcherState : IState
    {
        protected readonly PlayerArcherStateMachine ArcherStateMachine;
        protected readonly PlayerArcherGroundedData ArcherMovementData;
        protected PlayerArcherState(PlayerArcherStateMachine archerStateMachine)
        {
            ArcherStateMachine = archerStateMachine;
            ArcherMovementData = ArcherStateMachine.PlayerArcher.Data.ArcherGroundedData;
            InitializeData();
        }

        private void InitializeData() => ArcherStateMachine.ArcherStateReusableData.TimeToReachTargetRotation.y =
            ArcherMovementData.BaseArcherRotationData.TargetRotationReachTime;

        #region IState Methods
        public virtual void Enter()
        {
            Debug.Log("PlayerArcherState Enter: " + GetType().Name);
            AddInputActionCallbacks();
        }

        public virtual void Exit()
        {
            RemoveInputActionCallbacks();
        }

        public virtual void HandleInput() => ReadMovementInput();
        public virtual void Update(){}
        public virtual void PhysicsUpdate() => AddForceToPlayer();
        public virtual void OnAnimationEnterEvent(){}
        public virtual void OnAnimationExitEvent(){}
        public virtual void OnAnimationTransitionEvent(){}
        public virtual void OnTriggerEnter(Collider collider){}
        public virtual void OnTriggerExit(Collider collider){}
        #endregion

        #region Main Methods
        private void ReadMovementInput() => 
            ArcherStateMachine.ArcherStateReusableData.MovementInput = ArcherStateMachine.PlayerArcher.ArcherInput.PlayerArcherActions.Movement.ReadValue<Vector2>();

        protected void AddForceToPlayer() //Move
        {
            if (ArcherStateMachine.ArcherStateReusableData.MovementInput == Vector2.zero ||
                ArcherStateMachine.ArcherStateReusableData.MovementSpeedModifier == 0f)
                return;
            
            var movementDirection = GetMovementInputDirection();
            
            var targetRotationYAngle = RotatePlayer(movementDirection);
            
            var targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);
            
            var movementSpeed = GetMovementSpeed();
            
            var currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();
            
            ArcherStateMachine.PlayerArcher.Rigidbody.AddForce(
                targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
        }
        
        private float RotatePlayer(Vector3 direction)
        {
            var directionAngle = UpdateTargetRotation(direction);

            RotateTowardsTargetRotation();

            return directionAngle;
        }
        
        private static float GetDirectionAngle(Vector3 direction)
        {
            var directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            if(directionAngle < 0)
                directionAngle += 360f;
            return directionAngle;
        }

        private float AddCameraToRotationAngle(float angle)
        {
            angle += ArcherStateMachine.PlayerArcher.MainCameraTransform.eulerAngles.y;

            if(angle > 360f)
                angle -= 360f;
            return angle;
        }
        
        private void UpdateTargetRotationData(float targetAngle)
        {
            ArcherStateMachine.ArcherStateReusableData.CurrentTargetRotation.y = targetAngle;
            ArcherStateMachine.ArcherStateReusableData.DampedTargetRotationPassedTime.y = 0f;
        }
        #endregion

        #region Reusable Methods
        private Vector3 GetPlayerHorizontalVelocity()
        {
            var playerHorizontalVelocity = ArcherStateMachine.PlayerArcher.Rigidbody.velocity;
            playerHorizontalVelocity.y = 0f;

            return playerHorizontalVelocity;
        }

        private void RotateTowardsTargetRotation()
        {
            var currentYAngle = ArcherStateMachine.PlayerArcher.Rigidbody.rotation.eulerAngles.y;
            
            if(Mathf.Approximately(currentYAngle, ArcherStateMachine.ArcherStateReusableData.CurrentTargetRotation.y))
                return;
            
            var smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle,
                ArcherStateMachine.ArcherStateReusableData.CurrentTargetRotation.y, 
                ref ArcherStateMachine.ArcherStateReusableData.DampedTargetRotationCurrentVelocity.y,
                ArcherStateMachine.ArcherStateReusableData.TimeToReachTargetRotation.y -
                ArcherStateMachine.ArcherStateReusableData.DampedTargetRotationCurrentVelocity.y);
            
            ArcherStateMachine.ArcherStateReusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;
            
            var targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);
            
            ArcherStateMachine.PlayerArcher.Rigidbody.MoveRotation(targetRotation);
        }
        
        private float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
        {
            var directionAngle = GetDirectionAngle(direction);
            
            if(shouldConsiderCameraRotation)
                directionAngle = AddCameraToRotationAngle(directionAngle);
            
            if(Mathf.Approximately(directionAngle, ArcherStateMachine.ArcherStateReusableData.CurrentTargetRotation.y)) 
                UpdateTargetRotationData(directionAngle);

            return directionAngle;
        }

        private Vector3 GetTargetRotationDirection(float targetAngle) => 
            Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        
        protected void ResetVelocity() => ArcherStateMachine.PlayerArcher.Rigidbody.velocity = Vector3.zero;

        private Vector3 GetMovementInputDirection() => new(ArcherStateMachine.ArcherStateReusableData.MovementInput.x,
            0f, ArcherStateMachine.ArcherStateReusableData.MovementInput.y);
        private float GetMovementSpeed() => ArcherMovementData.BaseSpeed * ArcherStateMachine.ArcherStateReusableData.MovementSpeedModifier;
        
        protected virtual void AddInputActionCallbacks() => 
            ArcherStateMachine.PlayerArcher.ArcherInput.PlayerArcherActions.WalkToggle.started += OnWalkToggleStarted;

        protected virtual void RemoveInputActionCallbacks() => 
            ArcherStateMachine.PlayerArcher.ArcherInput.PlayerArcherActions.WalkToggle.started -= OnWalkToggleStarted;
        #endregion

        #region Input Methods
        protected virtual void OnWalkToggleStarted(InputAction.CallbackContext context) =>
            ArcherStateMachine.ArcherStateReusableData.ShouldWalk = !ArcherStateMachine.ArcherStateReusableData.ShouldWalk;
        #endregion
    }
}