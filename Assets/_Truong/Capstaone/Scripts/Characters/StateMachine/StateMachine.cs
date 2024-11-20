using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Truong
{
    public abstract class StateMachine
    {
        protected IState currentState;

        public void ChangeState(IState state)
        {
            currentState?.Exit();
            currentState = state;
            currentState?.Enter();
        }

        public void HandleInput()
        {
            currentState.HandleInput();
        }

        public void UpDate()
        {
            currentState.UpDate();
        }

        public void PhysicUpdate()
        {
            currentState.PhysicUpdate();
        }

        public void OnAnimationEnterEvent()
        {
            currentState?.OnAnimationEnterEvent();
        }

        public void OnAnimationExitEvent()
        {
            currentState?.OnAnimationExitEvent();
        }

        public void OnAnimationTransitionEvent()
        {
            currentState?.OnAnimationTransitionEvent();
        }
    }

}
