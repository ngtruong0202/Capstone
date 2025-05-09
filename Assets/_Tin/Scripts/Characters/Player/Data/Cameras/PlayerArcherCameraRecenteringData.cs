using System;
using UnityEngine;

namespace _Tin.Scripts.Characters.Player.Data.Cameras
{
    [Serializable]
    public class PlayerArcherCameraRecenteringData
    {
        [field: SerializeField][field: Range(0f, 360f)] public float MinimumAngle { get; private set; }
        [field: SerializeField][field: Range(0f, 360f)] public float MaximumAngle { get; private set; }
        [field: SerializeField][field: Range(-1f, 20f)] public float WaitTime { get; private set; }
        [field: SerializeField][field: Range(-1f, 20f)] public float RecenteringTime { get; private set; }

        public bool IsWithinRange(float angle) => angle >= MinimumAngle && angle <= MaximumAngle;
    }
}
