using System;
using UnityEngine;

namespace _Tin.Scripts.Characters.Player.Data.States.Grounded.Landing
{
    [Serializable]
    public class PlayerArcherRollData
    {
        [field: SerializeField][field: Range(0f, 3f)] public float SpeedModifier { get; private set; } = 1f;
    }
}
