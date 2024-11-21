using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Truong
{
    public class PlayerSkillQState : PlayerEventAttactState
    {
        public PlayerSkillQState(PlayerAttackStateMachine playerAttackState) : base(playerAttackState)
        {
        }

        public override void Enter()
        {
            base.Enter();

            PlayerSkillSO skill = stateMachine.Player.SkillManager.skillSO[0];

            stateMachine.Player.SkillManager.OnSkill(skill);

            StartAnimation(stateMachine.Player.PlayerAnimation.skill, 1);
        }

       
    }
}


