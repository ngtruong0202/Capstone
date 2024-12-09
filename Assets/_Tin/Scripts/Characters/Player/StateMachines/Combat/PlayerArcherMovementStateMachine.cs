using _Tin.Scripts.Characters.Player.StateMachines.Combat.States.Grounded;
using _Tin.Scripts.Characters.Player.StateMachines.Combat.States.Grounded.Moving;
using UnityEngine;

namespace _Tin.Scripts.Characters.Player.StateMachines.Combat
{
    public class PlayerArcherMovementStateMachine : StateMachine
    {
        public PlayerArcherIdlingState ArcherIdlingState { get; }
        public PlayerArcherWalkingState ArcherWalkingState { get; }
        public PlayerArcherRunningState ArcherRunningState { get; }
        public PlayerArcherSprintingState ArcherSprintingState { get; }

        public PlayerArcherMovementStateMachine()
        {
            ArcherIdlingState = new PlayerArcherIdlingState();
            ArcherWalkingState = new PlayerArcherWalkingState();
            ArcherRunningState = new PlayerArcherRunningState();
            ArcherSprintingState = new PlayerArcherSprintingState();
        }
    }
}