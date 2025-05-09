using System;
using System.Collections.Generic;
using _Tin.Scripts.Characters.Player.Data.Cameras;
using UnityEngine;

namespace _Tin.Scripts.Characters.Player.Data.States
{
    [Serializable]
    public class PlayerArcherIdleData
    {
        [field: SerializeField]
        public List<PlayerArcherCameraRecenteringData> BackwardsCameraRecenteringData { get; private set; }
    }
}
