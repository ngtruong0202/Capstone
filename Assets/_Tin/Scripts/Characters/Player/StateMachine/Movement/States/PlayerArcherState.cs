using UnityEngine;

namespace _Tin.Scripts.Characters.Player.StateMachine.Movement.States
{
    public class PlayerArcherState : IState
    {
        private readonly PlayerArcherStateMachine _archerStateMachine;
        private Vector2 MovementInput { get; set; }
        private const float BaseSpeed = 5f;
        private const float SpeedModifier = 1f;

        protected PlayerArcherState(PlayerArcherStateMachine archerStateMachine) => 
            _archerStateMachine = archerStateMachine;

        #region IState Methods
        public virtual void Enter() => Debug.Log("PlayerArcherState Enter: " + GetType().Name);
        public virtual void Exit(){}
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

        private void AddForceToPlayer() //Move
        {
            if (MovementInput == Vector2.zero || SpeedModifier == 0f)
                return;
            
            var movementDirection = GetMovementDirection();
            var movementSpeed = GetMovementSpeed();
            
            Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();
            
            _archerStateMachine.PlayerArcher.Rigidbody.AddForce(movementDirection * (5F * movementSpeed) - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
        }
        #endregion

        #region Reusable Methods
        private Vector3 GetPlayerHorizontalVelocity()
        {
            var playerHorizontalVelocity = _archerStateMachine.PlayerArcher.Rigidbody.velocity;
            playerHorizontalVelocity.y = 0f;

            return playerHorizontalVelocity;
        }
        private Vector3 GetMovementDirection() => new(MovementInput.x, 0f, MovementInput.y);
        private static float GetMovementSpeed() => BaseSpeed * SpeedModifier;
        #endregion
    }
}