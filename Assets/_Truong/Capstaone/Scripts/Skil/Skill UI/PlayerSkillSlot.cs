using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

namespace Truong
{
    public class PlayerSkillSlot : MonoBehaviour
    {
        public Player Player;
        private PlayerSkillSO skillSO;

        [SerializeField] private int slotId;

        [SerializeField] private Image iconSkill;
        [SerializeField] private Image panelSkill;

        [SerializeField] private TextMeshProUGUI skillCooldown;

        private bool isCooldown = false;

        private void Start()
        {
            panelSkill.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            GameManager.PlayerSpawned += CreateSkillUI;
        }

        private void OnDisable()
        {
            GameManager.PlayerSpawned -= CreateSkillUI;
        }

        private void Update()
        {
            if (skillSO == null) return;

            float deltaTime = Time.deltaTime;

            if (skillSO.isCooldown)
            {
                panelSkill.gameObject.SetActive(true);
                skillSO.UpdateCooldown(deltaTime);

                if (!skillSO.isCooldown && skillSO.cooldownRemaining == 0)
                {
                    StopCooldown(skillSO);
                }
            }

            if (isCooldown)
            {
                UpdateSkillUI();
            }
        }


        public void CreateSkillUI(Player player)
        {
            Player = player;
  
            if (player.Data.SkillData != null)
            {
                PlayerSkillSO _skillSO = GetSkill(player.Data.SkillData);

                skillSO = _skillSO;

                iconSkill.sprite = skillSO.skillImage;
                panelSkill.gameObject.SetActive(false);
            }
        }

        public PlayerSkillSO GetSkill(PlayerSkillSO[] skillSOs)
        {
            for (int i = 0; i < skillSOs.Length; i++)
            {
                if (skillSOs[i].skillID == slotId)
                {
                    return skillSOs[i];
                }
            }

            return null;
        }

        public void UpdateSkillUI()
        {
            skillCooldown.text = skillSO.cooldownRemaining.ToString("0.0");
        }

        public void StopCooldown(PlayerSkillSO skill)
        {
            isCooldown = false;
            panelSkill.gameObject.SetActive(false);
        }

    }
}
