using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Truong
{

    [CreateAssetMenu(fileName = "Pool", menuName = "pool")]
    public class Pool : ScriptableObject
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public Transform pos;
    }
}

