using System;
using UnityEngine;

namespace _Tin.Scripts.Characters.Player.Data.Colliders
{
    [Serializable]
    public class PlayerArcherTriggerColliderData
    {
        [field: SerializeField] public BoxCollider GroundCheckCollider { get; private set; }

        public Vector3 GroundCheckColliderExtends { get; private set; }

        public void Initialize() => 
            GroundCheckColliderExtends = GroundCheckCollider.bounds.extents;
    }
}
