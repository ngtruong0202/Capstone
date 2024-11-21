using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Truong
{
    public class PlayerSkillManager : MonoBehaviour
    {
        public Player Player;
        public PlayerAttackStateMachine attackStateMachine;


        public List<PlayerSkillSO> skillSO = new List<PlayerSkillSO>();
        

        private void Awake()
        {
            Player = GetComponent<Player>();
            attackStateMachine = new PlayerAttackStateMachine(Player);
        }

        private void Start()
        {
            
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

        public void OnSkill(PlayerSkillSO SkillData)
        {
            if (SkillData.isCooldown)
            {
                Debug.Log("Skill is on cooldown! Time left: " + Mathf.Ceil(SkillData.cooldownRemaining) + "s");
                return;
            }

            
            Debug.Log("Skill Activated!");
            StartCoroutine(StartCooldown(SkillData));
        }

        private IEnumerator StartCooldown(PlayerSkillSO SkillData)
        {
            SkillData.isCooldown = true;
            SkillData.cooldownRemaining = SkillData.cooldownTime;

            while (SkillData.cooldownRemaining > 0)
            {
                SkillData.cooldownRemaining -= Time.deltaTime;
                yield return null;
            }

            SkillData.isCooldown = false;
            Debug.Log("Skill is ready to use!");
        }

    }
}

