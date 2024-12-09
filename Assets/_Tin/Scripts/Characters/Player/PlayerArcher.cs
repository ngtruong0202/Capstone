using System;
using _Tin.Scripts.Characters.Player.StateMachines.Combat;
using UnityEngine;

namespace _Tin.Scripts.Characters.Player
{
    public class PlayerArcher : MonoBehaviour
    {
        private PlayerArcherMovementStateMachine _archerMovementStateMachine;
        
        private void Awake() => _archerMovementStateMachine = new PlayerArcherMovementStateMachine();

        private void Start() => _archerMovementStateMachine.ChangeState(_archerMovementStateMachine.ArcherIdlingState);

        private void Update()
        {
            _archerMovementStateMachine.HandleInput();
            _archerMovementStateMachine.Update();
        }
        
        private void FixedUpdate() => _archerMovementStateMachine.PhysicsUpdate();
    }
}