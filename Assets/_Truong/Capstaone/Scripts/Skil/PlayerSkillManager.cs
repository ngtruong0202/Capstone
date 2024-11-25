using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Truong
{
    [Serializable]
    public class PlayerSkillManager : MonoBehaviour
    {
        public Player Player;
        
        public delegate void StartSkill(PlayerSkillSO skillSO);
        public static event StartSkill OnStartSkill;


        public delegate void SkillEvent(PlayerSkillSO skill);
        public static event SkillEvent OnSkillCooldownStart;
        public static event SkillEvent OnSkillCooldownEnd;


        private void Awake()
        {
            Player = GetComponent<Player>();    
        }

        public void UseSkill(int skillId)
        {
            PlayerSkillSO skill = Player.Data.SkillData[skillId];

            if (skill != null && skill.CanUse())
            {
                skill.StartCooldown();
                OnSkillCooldownStart?.Invoke(skill);
            }
        }

        public void AttackEffect(string effect)
        {
            GameObject obj = PoolManager.Instance.GetPooledObject(effect);
            if (obj != null)
            {
                obj.transform.position = transform.position;
                obj.transform.rotation = transform.rotation;
                obj.SetActive(true);
            }
        }

        public void AttackEffectParent(string effect)
        {
            GameObject obj = PoolManager.Instance.GetPooledObject(effect);
            if (obj != null)
            {
                obj.transform.parent = null;
                obj.transform.position = transform.position;
                obj.transform.rotation = transform.rotation;
                obj.SetActive(true);
            }
        }
    }
}

