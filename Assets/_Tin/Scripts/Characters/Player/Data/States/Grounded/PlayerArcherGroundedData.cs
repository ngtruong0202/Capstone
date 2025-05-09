using System;
using System.Collections.Generic;
using _Tin.Scripts.Characters.Player.Data.Cameras;
using _Tin.Scripts.Characters.Player.Data.States.Grounded.Landing;
using _Tin.Scripts.Characters.Player.Data.States.Grounded.Moving;
using _Tin.Scripts.Characters.Player.Data.States.Grounded.Stopping;
using UnityEngine;

namespace _Tin.Scripts.Characters.Player.Data.States.Grounded
{
    [Serializable]
    public class PlayerArcherGroundedData
    {
        [field: SerializeField] [field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 5f;
        [field: SerializeField] [field: Range(0f, 5f)] public float GroundToFallRayDistance { get; private set; } = 1f;
        [field: SerializeField] public List<PlayerArcherCameraRecenteringData> SidewaysCameraRecenteringData { get; private set; }
        [field: SerializeField] public List<PlayerArcherCameraRecenteringData> BackwardsCameraRecenteringData { get; private set; }
        [field: SerializeField] public AnimationCurve SlopeSpeedAngles { get; private set; }
        [field: SerializeField] public PlayerArcherRotationData BaseArcherRotationData { get; private set; }
        [field: SerializeField] public PlayerArcherIdleData ArcherIdleData { get; private set; }
        [field: SerializeField] public PlayerArcherWalkData ArcherWalkData { get; private set; }
        [field: SerializeField] public PlayerArcherRunData ArcherRunData { get; private set; }
        [field: SerializeField] public PlayerArcherSprintData ArcherSprintData { get; private set; }
        [field: SerializeField] public Moving.PlayerArcherDashData ArcherDashData { get; private set; }
        [field: SerializeField] public PlayerArcherStopData ArcherStopData { get; private set; }
        [field: SerializeField] public PlayerArcherRollData ArcherRollData { get; private set; }
    }
}
