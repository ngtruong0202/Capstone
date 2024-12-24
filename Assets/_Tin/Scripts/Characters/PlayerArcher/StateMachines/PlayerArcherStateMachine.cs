using _Tin.Scripts.Characters.PlayerArcher.Data.States;
using UnityEngine;

namespace _Tin.Scripts.Characters.PlayerArcher.StateMachines
{
    public class PlayerArcherStateMachine : MonoBehaviour
    {
        public Player.PlayerArcher PlayerArcher { get; }
        
        public PlayerArcherStateReusableData ArcherReusableData { get; }
        
        public PlayerArcherStateMachine(Player.PlayerArcher playerArcher)
        {
            PlayerArcher = playerArcher;
            ArcherReusableData = new PlayerArcherStateReusableData();
        }
    }
}