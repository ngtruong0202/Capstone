using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Truong
{
    public class PlayerAttackCombo4State : PlayerEventAttactState
    {
        public PlayerAttackCombo4State(PlayerAttackStateMachine playerAttackState) : base(playerAttackState)
        {
        }

        public override void Enter()
        {
            base.Enter();

            stateMachine.Player.Rigidbody.transform.position += stateMachine.Player.Rigidbody.transform.forward * 3f;

            if (stateMachine.Player.EnemyDetection.currentTarget != null)
            {
                RotateTowardsTarget(stateMachine.Player.EnemyDetection.currentTarget.position);
            }

            StartAnimation(stateMachine.Player.PlayerAnimation.attack, 4);

        }
    }
}

