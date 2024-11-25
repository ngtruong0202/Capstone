
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
            if (stateMachine.Player.Data.SkillData[0].isCooldown) return;

            stateMachine.ChangeState(stateMachine.SkillQState);
        }

        private void OnSkillEStarted(InputAction.CallbackContext context)
        {
            if (stateMachine.Player.Data.SkillData[1].isCooldown) return;

            stateMachine.ChangeState(stateMachine.SkillEState);
        }

        private void OnSkillRStarted(InputAction.CallbackContext context)
        {
            if (stateMachine.Player.Data.SkillData[2].isCooldown) return;

            stateMachine.ChangeState(stateMachine.SkillRState);
        }

        private void OnSkillTStarted(InputAction.CallbackContext context)
        {
            if (stateMachine.Player.Data.SkillData[3].isCooldown) return;

            stateMachine.ChangeState(stateMachine.SkillTState);
        }
    }
}

