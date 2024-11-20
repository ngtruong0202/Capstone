using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAttackData
{
    [field: SerializeField][field: Range(0, 2)] public float CooldownTime { get; private set; } = 1f;
    [field: SerializeField][field: Range(0, 3)] public float ResetAttackTime { get; private set; } = 2f;
}
