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

            stateMachine.Player.SkillManager.UseSkill(0);

            StartAnimation(stateMachine.Player.PlayerAnimation.skill, 1);
        }

       
    }
}


