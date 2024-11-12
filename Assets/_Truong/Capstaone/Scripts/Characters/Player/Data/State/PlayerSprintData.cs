
using System;
using UnityEngine;

namespace Truong
{
    [Serializable]
    public class PlayerSprintData
    {
        [field: SerializeField][field: Range(1f, 3f)] public float SpeedMotifier { get; private set; } = 1.7f;
        [field: SerializeField][field: Range(1f, 3f)] public float SprintToRunTime { get; private set; } = 1f;

    }
}

