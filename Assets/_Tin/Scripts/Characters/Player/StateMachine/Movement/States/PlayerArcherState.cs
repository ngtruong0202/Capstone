using UnityEngine;
using UnityEngine.InputSystem;

namespace _Tin.Scripts.Characters.Player.StateMachine.Movement.States
{
    public class PlayerArcherState : IState
    {
        protected readonly PlayerArcherStateMachine _archerStateMachine;
        protected Vector2 MovementInput { get; private set; }
        private const float BaseSpeed = 5f;
        protected float SpeedModifier = 1f;

        private Vector3 _currentTargetRotation;
        private Vector3 _timeToReachTargetRotation;
        private Vector3 _dampedTargetRotationCurrentVelocity;
        private Vector3 _dampedTargetRotationPassedTime;

        protected bool shouldWalk;

        protected PlayerArcherState(PlayerArcherStateMachine archerStateMachine)
        {
            _archerStateMachine = archerStateMachine;
            InitializeData();
        }

        private void InitializeData() => _timeToReachTargetRotation.y = 0.14f;

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
            MovementInput = _archerStateMachine.PlayerArcher.ArcherInput.PlayerArcherActions.Movement.ReadValue<Vector2>();

        protected void AddForceToPlayer() //Move
        {
            if (MovementInput == Vector2.zero || SpeedModifier == 0f)
                return;
            
            var movementDirection = GetMovementInputDirection();
            
            var targetRotationYAngle = RotatePlayer(movementDirection);
            
            var targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);
            
            var movementSpeed = GetMovementSpeed();
            
            var currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();
            
            _archerStateMachine.PlayerArcher.Rigidbody.AddForce(
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
            angle += _archerStateMachine.PlayerArcher.MainCameraTransform.eulerAngles.y;

            if(angle > 360f)
                angle -= 360f;
            return angle;
        }
        
        private void UpdateTargetRotationData(float targetAngle)
        {
            _currentTargetRotation.y = targetAngle;
            _dampedTargetRotationPassedTime.y = 0f;
        }
        #endregion

        #region Reusable Methods
        private Vector3 GetPlayerHorizontalVelocity()
        {
            var playerHorizontalVelocity = _archerStateMachine.PlayerArcher.Rigidbody.velocity;
            playerHorizontalVelocity.y = 0f;

            return playerHorizontalVelocity;
        }

        private void RotateTowardsTargetRotation()
        {
            var currentYAngle = _archerStateMachine.PlayerArcher.Rigidbody.rotation.eulerAngles.y;
            
            if(Mathf.Approximately(currentYAngle, _currentTargetRotation.y))
                return;
            
            var smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, _currentTargetRotation.y, 
                ref _dampedTargetRotationCurrentVelocity.y, _timeToReachTargetRotation.y - _dampedTargetRotationCurrentVelocity.y);
            
            _dampedTargetRotationPassedTime.y += Time.deltaTime;
            
            var targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);
            
            _archerStateMachine.PlayerArcher.Rigidbody.MoveRotation(targetRotation);
        }
        
        private float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
        {
            var directionAngle = GetDirectionAngle(direction);
            
            if(shouldConsiderCameraRotation)
                directionAngle = AddCameraToRotationAngle(directionAngle);
            
            if(Mathf.Approximately(directionAngle, _currentTargetRotation.y)) 
                UpdateTargetRotationData(directionAngle);

            return directionAngle;
        }

        private Vector3 GetTargetRotationDirection(float targetAngle) => 
            Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        
        protected void ResetVelocity() => _archerStateMachine.PlayerArcher.Rigidbody.velocity = Vector3.zero;

        private Vector3 GetMovementInputDirection() => new(MovementInput.x, 0f, MovementInput.y);
        private float GetMovementSpeed() => BaseSpeed * SpeedModifier;
        
        protected virtual void AddInputActionCallbacks()
        {
            _archerStateMachine.PlayerArcher.ArcherInput.PlayerArcherActions.WalkToggle.started += OnWalkToggleStarted;
        }
        
        protected virtual void RemoveInputActionCallbacks()
        {
            _archerStateMachine.PlayerArcher.ArcherInput.PlayerArcherActions.WalkToggle.started -= OnWalkToggleStarted;
        }
        #endregion

        #region Input Methods
        protected virtual void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            shouldWalk = !shouldWalk;
        }
        #endregion
    }
}