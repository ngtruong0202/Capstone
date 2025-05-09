using System;
using UnityEngine;

namespace _Tin.Scripts.Characters.Player.Data.States.Grounded.Moving
{
    [Serializable]
    public class PlayerArcherSprintData
    {
        [field: SerializeField][field: Range(1f, 3f)] public float SpeedModifier { get; private set; } = 1.7f;
        [field: SerializeField][field: Range(0f, 5f)] public float SprintToRunTime { get; private set; } = 1f;
        [field: SerializeField][field: Range(0f, 2f)] public float RunToWalkTime { get; private set; } = 0.5f;
    }
}
