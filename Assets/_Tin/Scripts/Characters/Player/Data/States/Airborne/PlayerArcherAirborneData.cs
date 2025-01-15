using System;
using UnityEngine;

namespace _Tin.Scripts.Characters.Player.Data.States.Airborne
{
    [Serializable]
    public class PlayerArcherAirborneData
    {
        [field: SerializeField] public PlayerArcherJumpData JumpData { get; private set; }
        [field: SerializeField] public PlayerArcherFallData FallData { get; private set; }
    }
}
