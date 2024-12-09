using UnityEngine;

namespace _Tin.Scripts.Characters.Player.StateMachines.Combat.States
{
    public class PlayerArcherMovementState : IState
    {
        public virtual void Enter()
        {
            Debug.Log("State: " + GetType().Name);
        }

        public virtual void Exit()
        {
        }

        public virtual void HandleInput()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void PhysicsUpdate()
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

        public virtual void OnTriggerEnter(Collider collider)
        {
        }

        public virtual void OnTriggerExit(Collider collider)
        {
        }
    }
}