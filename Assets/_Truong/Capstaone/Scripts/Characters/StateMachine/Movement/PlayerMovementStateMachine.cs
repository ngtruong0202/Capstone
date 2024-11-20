using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Truong
{
    public class PlayerMovementStateMachine : StateMachine
    {
        public Player Player;
        public PlayerReusableData ReusableData { get; }

        public PlayerIdlingState IdlingState { get; }
        public PlayerRunningState RunningState { get; }
        public PlayerJumpingState JumpingState { get; }
        public PlayerSprintState SprintingState { get; }
        public PlayerDashingState DashingState { get; }

        //public PlayerBasicAttackState AttackingState { get; }


        public PlayerMovementStateMachine(Player player)
        {
            Player = player;

            ReusableData = new PlayerReusableData();

            IdlingState = new PlayerIdlingState(this);
            RunningState = new PlayerRunningState(this);
            JumpingState = new PlayerJumpingState(this);
            SprintingState = new PlayerSprintState(this);
            DashingState = new PlayerDashingState(this);

            //AttackingState = new PlayerBasicAttackState(this);
        }
    }

}

