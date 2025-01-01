using UnityEngine;

namespace _Tin.Scripts.Characters.Player.StateMachine.Movement.States
{
    public class PlayerArcherState : IState
    {
        public PlayerArcherStateMachine ArcherStateMachine;
        
        protected Vector2 MovementInput { get; private set; }
        protected float BaseSpeed = 5f;
        protected float SpeedModifier = 1f;
        
        public PlayerArcherState(PlayerArcherStateMachine archerStateMachine) => 
            ArcherStateMachine = archerStateMachine;

        #region IState Methods
        public virtual void Enter() => Debug.Log("PlayerArcherState Enter: " + GetType().Name);
        public virtual void Exit(){}
        public virtual void HandleInput() => ReadMovementInput();
        public virtual void Update(){}
        public virtual void PhysicsUpdate(){}
        public virtual void OnAnimationEnterEvent(){}
        public virtual void OnAnimationExitEvent(){}
        public virtual void OnAnimationTransitionEvent(){}
        public virtual void OnTriggerEnter(Collider collider){}
        public virtual void OnTriggerExit(Collider collider){}
        #endregion

        #region Main Methods
        private void ReadMovementInput() => 
            MovementInput = ArcherStateMachine.PlayerArcher.ArcherInput.PlayerArcherActions.Movement.ReadValue<Vector2>();

        private void Move()
        {
            if (MovementInput == Vector2.zero || SpeedModifier == 0f)
                return;
            
            Vector3 movementDirection = GetMovementDirection();
            float movementSpeed = GetMovementSpeed();
            
            ArcherStateMachine.PlayerArcher.Rigidbody.AddForce(movementDirection * movementSpeed, ForceMode.VelocityChange);
        }
        #endregion

        #region Reusable Methods
        protected Vector3 GetMovementDirection() => new(MovementInput.x, 0f, MovementInput.y);
        protected float GetMovementSpeed() => BaseSpeed * SpeedModifier;
        
        #endregion
    }
}