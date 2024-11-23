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

            PlayerSkillSO skill = stateMachine.Player.SkillManager.skillSO[2];

            stateMachine.Player.SkillManager.OnSkill(skill);

            StartAnimation(stateMachine.Player.PlayerAnimation.skill, 1);
        }
    }
}

