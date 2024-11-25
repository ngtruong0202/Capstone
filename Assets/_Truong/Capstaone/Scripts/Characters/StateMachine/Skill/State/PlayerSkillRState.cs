using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Truong
{
    public class PlayerSkillRState : PlayerEventAttactState
    {
        public PlayerSkillRState(PlayerAttackStateMachine playerAttackState) : base(playerAttackState)
        {
        }

        public override void Enter()
        {
            base.Enter();

            stateMachine.Player.SkillManager.UseSkill(2);

            StartAnimation(stateMachine.Player.PlayerAnimation.skill, 3);
        }
    }
}

