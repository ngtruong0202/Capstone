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

            stateMachine.Player.SkillManager.UseSkill(3);

            StartAnimation(stateMachine.Player.PlayerAnimation.skill, 4);
        }
    }
}

