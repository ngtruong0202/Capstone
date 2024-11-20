using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Truong
{
    public class PlayerMovementState : IState
    {
        protected PlayerMovementStateMachine stateMachine;

        protected PlayerGroundedData movementData;


        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
        {
            stateMachine = playerMovementStateMachine;

            movementData = stateMachine.Player.Data.GroundedData;

            InitialiazeData();
        }

        public PlayerMovementState(PlayerMovementState movementState)
        {
            movementState = this;
        }

        private void InitialiazeData()
        {
            stateMachine.ReusableData.TimeToReachTargetRotation = movementData.BaseRotationData.TargetRotationReachTime;
        }

        public virtual void Enter()
        {
            Debug.Log("State : " + GetType().Name);

            AddInputActionCallbacks();
        }

        public virtual void Exit()
        {
            RemoveInputActionCallbacks();
        }

        public virtual void HandleInput()
        {
            ReadMovementInput();
        }

        public virtual void PhysicUpdate()
        {
            Move();


        }

        public virtual void UpDate()
        {

        }

        protected void StartAnimation(int animationHash)
        {
            stateMachine.Player.Animator.SetBool(animationHash, true);
        }

        protected void StopAnimation(int animationHash)
        {
            stateMachine.Player.Animator.SetBool(animationHash, false);
        }


        #region Rotation
        protected float Rotate(Vector3 direction)
        {
            float directionAngle = UpdateTargetRotation(direction);

            RotateTowardsTargetRotation();

            return directionAngle;
        }

        private float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
        {
            float directionAngle = DirectionAngle(direction);

            if (shouldConsiderCameraRotation)
            {
                directionAngle = AddCameraRotationToAngle(directionAngle);
            }

            if (directionAngle != stateMachine.ReusableData.CurrentTargetRotation.y)
            {
                UpdateTargetRotationData(directionAngle);
            }

            return directionAngle;
        }

        private void UpdateTargetRotationData(float targetAngle)
        {
            stateMachine.ReusableData.CurrentTargetRotation.y = targetAngle;

            stateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y = 0f;
        }

        protected void RotateTowardsTargetRotation()
        {
            float currentYAngle = stateMachine.Player.Rigidbody.rotation.eulerAngles.y;

            if (currentYAngle == stateMachine.ReusableData.CurrentTargetRotation.y)
            {
                return;
            }

            float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, stateMachine.ReusableData.CurrentTargetRotation.y,
                ref stateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y, stateMachine.ReusableData.TimeToReachTargetRotation.y
                - stateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y);

            stateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y += Time.deltaTime;

            Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);
            stateMachine.Player.Rigidbody.MoveRotation(targetRotation);

        }

        protected float AddCameraRotationToAngle(float angle)
        {
            angle += stateMachine.Player.MainCameraTransform.eulerAngles.y;

            if (angle > 360)
            {
                angle -= 360;
            }

            return angle;
        }

        protected float DirectionAngle(Vector3 direction)
        {
            float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            if (directionAngle < 0)
            {
                directionAngle += 360;
            }

            return directionAngle;
        }
        #endregion

        #region Movement
        private void ReadMovementInput()
        {
            stateMachine.ReusableData.MovementInput = stateMachine.Player.Inputs.playerActions.Movement.ReadValue<Vector2>();

        }

        private void Move()
        {
            if (stateMachine.ReusableData.MovementInput == Vector2.zero || stateMachine.ReusableData.MovementSpeedModifier == 0)
            {
                return;
            }

            Vector3 movementDirection = GetMovementInputDirection();

            //xoay
            float targetRotationYAngle = Rotate(movementDirection);
            Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);

            float movementSpeed = GetMovementSpeed();
            Vector3 currentPlayerHorizontalVertical = GetPlayerHorizontalVertical();

            stateMachine.Player.Rigidbody.AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVertical, ForceMode.VelocityChange);
        }

        private Vector3 GetTargetRotationDirection(float targetAngle)
        {
            return Quaternion.Euler(0f, targetAngle, 0) * Vector3.forward;
        }

        private Vector3 GetPlayerHorizontalVertical()
        {
            Vector3 playerHorizontalVertical = stateMachine.Player.Rigidbody.velocity;
            playerHorizontalVertical.y = 0f;
            return playerHorizontalVertical;
        }

        protected float GetMovementSpeed()
        {
            return movementData.BaseSpeed * stateMachine.ReusableData.MovementSpeedModifier;
        }

        protected Vector3 GetMovementInputDirection()
        {
            return new Vector3(stateMachine.ReusableData.MovementInput.x, 0, stateMachine.ReusableData.MovementInput.y);
        }

        public void ResetVelocity()
        {
            stateMachine.Player.Rigidbody.velocity = Vector3.zero;
        }


        #endregion

        protected virtual void AddInputActionCallbacks()
        {

        }

        protected virtual void RemoveInputActionCallbacks()
        {

        }

        public virtual void OnAnimationEnterEvent()
        {

        }

        public virtual void OnAnimationExitEvent()
        {

        }

        public virtual void OnAnimationTransitionEvent()
        {

        }
    }
}
