using System;
using UnityEngine;

[Serializable]
public class PlayerAirborneData
{
    [field: SerializeField] public PlayerJumpData JumpData { get; private set; }
}
