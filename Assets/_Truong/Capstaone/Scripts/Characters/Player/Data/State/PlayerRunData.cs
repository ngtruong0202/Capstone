using System;
using UnityEngine;

namespace Truong
{
    [Serializable]
    public class PlayerRunData
    {
        [field: SerializeField][field: Range(0, 1)] public float SpeedModifier { get; private set; } = 1;
    }
}

