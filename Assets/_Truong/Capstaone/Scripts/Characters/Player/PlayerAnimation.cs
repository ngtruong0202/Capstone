using System;
using UnityEngine;

namespace Truong
{
    [Serializable]
    public class PlayerAnimation
    {
        [SerializeField] private string idling = "Idling";
        [SerializeField] private string movement = "Movement";
        [SerializeField] private string running = "Running";

        [SerializeField] private string sprint = "Sprint";
        [SerializeField] private string dashing = "Dashing";
        [SerializeField] private string jumping = "Jumping";


        public int IdlingHash { get; private set; }
        public int Movement { get; private set; }
        public int Running { get; private set; }
        public int DashingHash { get; private set; }
        public int JumpingHash { get; private set; }
        public int SprintHash { get; private set; }


        public void Initilize()
        {
            IdlingHash = Animator.StringToHash(idling);
            Movement = Animator.StringToHash(movement);
            Running = Animator.StringToHash(running);
            DashingHash = Animator.StringToHash(dashing);
            JumpingHash = Animator.StringToHash(jumping);
            SprintHash = Animator.StringToHash(sprint);
        }
    }
}

