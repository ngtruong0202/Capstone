using System;
using UnityEngine;

namespace _Tin.Scripts.Characters.Player.Data.States.Grounded.Moving
{
    [Serializable]
    public class PlayerArcherRunData
    {
        [field: SerializeField][field: Range(1f, 2f)] public float SpeedModifier { get; private set; } = 1f;
    }
}
