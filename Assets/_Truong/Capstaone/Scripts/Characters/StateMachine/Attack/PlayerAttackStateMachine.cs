using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Truong
{
    public class PlayerAttackStateMachine : StateMachine
    {
        public Player Player;

        public PlayerReusableData ReusableData { get; }

        public PlayerUnAttack UnAttack { get; }
        public PlayerBasicAttackState BasicAttackState {  get; }
        public PlayerAttackCombo3State PlayerAttackCombo3 {  get; }
        public PlayerAttackCombo4State PlayerAttackCombo4 {  get; }

        public PlayerSkillQState SkillQState { get; }
        public PlayerSkillEState SkillEState { get; }
        public PlayerSkillRState SkillRState { get; }
        public PlayerSkillTState SkillTState { get; }


        public PlayerAttackStateMachine(Player player)
        {
            Player = player;

            ReusableData = new PlayerReusableData();

            UnAttack = new PlayerUnAttack(this);
            BasicAttackState = new PlayerBasicAttackState(this);
            PlayerAttackCombo3 = new PlayerAttackCombo3State(this);
            PlayerAttackCombo4 = new PlayerAttackCombo4State(this);

            SkillQState = new PlayerSkillQState(this);
            SkillEState = new PlayerSkillEState(this);
            SkillRState = new PlayerSkillRState(this);
            SkillTState = new PlayerSkillTState(this);
        }
    }

}
