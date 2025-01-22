using System;
using UnityEngine;

namespace _Tin.Scripts.Characters.Player.Data.States
{
    [Serializable]
    public class PlayerArcherRotationData
    {
        [field: SerializeField] public float TargetRotationReachTime { get; private set; }
    }
}
