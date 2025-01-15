using System;
using UnityEngine;

namespace _Tin.Scripts.Characters.Player.Data.Layers
{
    [Serializable]
    public class PlayerArcherLayerData
    {
        [field: SerializeField] public LayerMask GroundLayer { get; private set; }

        public bool ContainsLayer(LayerMask layerMask, int layer) => (1 << layer & layerMask) != 0;

        public bool IsGroundLayer(int layer) => ContainsLayer(GroundLayer, layer);
    }
}
