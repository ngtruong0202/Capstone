
using System;
using UnityEngine;

namespace Truong
{
    [Serializable]

    public class PlayerDashData
    {
        [field: SerializeField][field: Range(1, 5)] public float SpeedModifier { get; private set; } = 2f;
        [field: SerializeField][field: Range(0f, 2f)] public float TimeToBeConsideredConsecutive { get; private set; } = 1f;
        [field: SerializeField][field: Range(1f, 10f)] public float ConsecutiveDashesLimitAmount { get; private set; } = 2f;
        [field: SerializeField][field: Range(0f, 5f)] public float DashLimitReachedCooldown { get; private set; } = 1.75f;
    }
}

