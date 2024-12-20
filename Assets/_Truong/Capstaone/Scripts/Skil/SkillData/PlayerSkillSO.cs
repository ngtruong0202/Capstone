using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Truong
{
    [CreateAssetMenu(fileName = "Skill", menuName = "CharacterController/Skill")]
    public class PlayerSkillSO : ScriptableObject
    {
        public Sprite skillImage;
        public KeyCode skillKey;

        public string skillName;
        public string skillType;
        public string skillDescription;

        public int skillID;
        public int skillLevel;

        public float cooldownTime;
        public float cooldownRemaining;

        public bool isCooldown = false;

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

