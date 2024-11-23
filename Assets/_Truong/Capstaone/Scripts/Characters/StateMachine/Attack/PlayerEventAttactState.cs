
using UnityEngine;
using UnityEngine.InputSystem;

namespace Truong
{
    public class PlayerEventAttactState : PlayerAttackState
    {
        public PlayerEventAttactState(PlayerAttackStateMachine playerAttackState) : base(playerAttackState)
        {
        }

       
        protected override void AddInputActionCallbacks()
        {
            stateMachine.Player.Inputs.playerActions.Attack.started += OnAttackStarted;

            stateMachine.Player.Inputs.playerActions.SkillQ.started += OnSkillQStarted;
            stateMachine.Player.Inputs.playerActions.SkillE.started += OnSkillEStarted;
            stateMachine.Player.Inputs.playerActions.SkillR.started += OnSkillRStarted;
            stateMachine.Player.Inputs.playerActions.SkillT.started += OnSkillTStarted;
        }

        protected override void RemoveInputActionCallbacks()
        {
            stateMachine.Player.Inputs.playerActions.Attack.started -= OnAttackStarted;

            stateMachine.Player.Inputs.playerActions.SkillQ.started -= OnSkillQStarted;
            stateMachine.Player.Inputs.playerActions.SkillE.started -= OnSkillEStarted;
            stateMachine.Player.Inputs.playerActions.SkillR.started -= OnSkillRStarted;
            stateMachine.Player.Inputs.playerActions.SkillT.started -= OnSkillTStarted;
        }


        private void OnAttackStarted(InputAction.CallbackContext context)
        {
            stateMachine.Player.Attack();
        }

        private void OnSkillQStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.SkillQState);
        }

        private void OnSkillEStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.SkillEState);
        }

        private void OnSkillRStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.SkillRState);
        }

        private void OnSkillTStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.SkillTState);
        }
    }
}

