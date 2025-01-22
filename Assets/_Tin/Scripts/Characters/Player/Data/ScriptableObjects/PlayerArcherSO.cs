using _Tin.Scripts.Characters.Player.Data.States.Airborne;
using _Tin.Scripts.Characters.Player.Data.States.Grounded;
using UnityEngine;

namespace _Tin.Scripts.Characters.Player.Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player", menuName = "Custom/Characters/PlayerArcher")]
    public class PlayerArcherSo : ScriptableObject
    {
        [field: SerializeField] public PlayerArcherGroundedData ArcherGroundedData { get; private set; } 
        [field: SerializeField] public PlayerArcherAirborneData ArcherAirborneData { get; private set; } 
    }
}
