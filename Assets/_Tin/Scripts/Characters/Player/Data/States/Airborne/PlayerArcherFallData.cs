using System;
using UnityEngine;

namespace _Tin.Scripts.Characters.Player.Data.States.Airborne
{
    [Serializable]
    public class PlayerArcherFallData
    {
        [field: SerializeField][field: Range(1f, 15f)] public float FallSpeedLimit { get; private set; } = 15f;
        [field: SerializeField][field: Range(0f, 100f)] public float MinimumDistanceToBeConsideredHardFall { get; private set; } = 3f;
    }
}
