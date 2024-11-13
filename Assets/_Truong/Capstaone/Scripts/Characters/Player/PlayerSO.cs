using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Truong
{
    [CreateAssetMenu(fileName = "Player", menuName = "CharacterController/Player")]
    public class PlayerSO : ScriptableObject
    {
        [field: SerializeField] public PlayerGroundedData GroundedData { get; private set; }

    }
}

