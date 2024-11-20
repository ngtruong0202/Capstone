using HutongGames.PlayMaker;
using System;
using System.Collections;
using System.Collections.Generic;
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
        }

        protected override void RemoveInputActionCallbacks()
        {
            stateMachine.Player.Inputs.playerActions.Attack.started -= OnAttackStarted;
        }

        private void OnAttackStarted(InputAction.CallbackContext context)
        {
           

            stateMachine.Player.Attack();
        }

       
    }
}

