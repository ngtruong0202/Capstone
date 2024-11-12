using System;
using UnityEngine;

namespace Truong
{
    [Serializable]
    public class PlayerGroundedData
    {
        [field: SerializeField][field: Range(0, 25)] public float BaseSpeed { get; private set; } = 5f;
        [field: SerializeField] public PlayerRotationData BaseRotationData { get; private set; }
        [field: SerializeField] public PlayerRunData RunData { get; private set; }
        [field: SerializeField] public PlayerDashData DashData { get; private set; }
        [field: SerializeField] public PlayerSprintData SprintData { get; private set; }
    }
}

