using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Truong
{
    public class PlayerSkillTState : PlayerEventAttactState
    {
        public PlayerSkillTState(PlayerAttackStateMachine playerAttackState) : base(playerAttackState)
        {
        }

        public override void Enter()
        {
            base.Enter();

            PlayerSkillSO skill = stateMachine.Player.SkillManager.skillSO[3];

            stateMachine.Player.SkillManager.OnSkill(skill);

            StartAnimation(stateMachine.Player.PlayerAnimation.skill, 4);
        }
    }
}

