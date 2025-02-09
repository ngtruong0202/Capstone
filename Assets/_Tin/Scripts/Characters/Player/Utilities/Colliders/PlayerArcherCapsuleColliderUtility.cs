using System;
using UnityEngine;

namespace _Tin.Scripts.Characters.Player.Utilities.Colliders
{
    [Serializable]
    public class PlayerArcherCapsuleColliderUtility : CapsuleColliderUtility
    {
        [field: SerializeField] public PlayerTriggerColliderData TriggerColliderData { get; private set; }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            TriggerColliderData.Initialize();
        }
    }
}
