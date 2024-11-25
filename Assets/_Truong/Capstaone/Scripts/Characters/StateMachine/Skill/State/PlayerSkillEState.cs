
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

            stateMachine.Player.SkillManager.UseSkill(1);

            StartAnimation(stateMachine.Player.PlayerAnimation.skill, 2);
        }
    }

}
