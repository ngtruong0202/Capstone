using System;
using System.Collections.Generic;
using _Tin.Scripts.Characters.Player.Data.Cameras;
using UnityEngine;

namespace _Tin.Scripts.Characters.Player.Data.States.Grounded.Moving
{
    [Serializable]
    public class PlayerArcherWalkData
    {
        [field: SerializeField][field: Range(0f, 1f)] public float SpeedModifier { get; private set; } = 0.25f;
        [field: SerializeField] public List<PlayerArcherCameraRecenteringData> BackwardsCameraRecenteringData { get; private set; }

    }
}
