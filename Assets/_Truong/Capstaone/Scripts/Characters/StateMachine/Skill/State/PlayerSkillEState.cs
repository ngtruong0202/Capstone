
using UnityEngine;


namespace Truong
{
    public class PlayerSkillEState : PlayerEventAttactState
    {
        public PlayerSkillEState(PlayerAttackStateMachine playerAttackState) : base(playerAttackState)
        {
        }
        public override void Enter()
        {
            base.Enter();

            PlayerSkillSO skill = stateMachine.Player.SkillManager.skillSO[1];

            stateMachine.Player.SkillManager.OnSkill(skill);

            StartAnimation(stateMachine.Player.PlayerAnimation.skill, 2);
        }
    }

}
