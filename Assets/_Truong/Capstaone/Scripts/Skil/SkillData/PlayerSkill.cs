
using Unity.VisualScripting;
using UnityEngine;

namespace Truong
{
    public class PlayerSkill : PlayerSkillSO
    {
        public bool CanUse()
        {
            return !isCooldown;
        }

        public void StartCooldown()
        {
            isCooldown = true;
            cooldownRemaining = cooldownTime;
        }

        public void UpdateCooldown(float deltaTime)
        {
            if (isCooldown)
            {
                cooldownRemaining -= deltaTime;
                if (cooldownRemaining <= 0)
                {
                    cooldownRemaining = 0;
                    isCooldown = false;
                }
            }
        }
    }
}


