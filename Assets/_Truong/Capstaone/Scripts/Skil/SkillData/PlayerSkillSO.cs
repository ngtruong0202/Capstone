using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


}
