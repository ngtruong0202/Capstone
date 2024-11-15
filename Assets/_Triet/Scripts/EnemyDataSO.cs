using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data Manager", menuName = "Enemy/EnemyDataManager")]
public class EnemyDataSO : ScriptableObject
{
    public List<EnemyData> datas = new List<EnemyData>();
}

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    public EnemyRace race;
    public float hp;
    public float defense;
    public int attack;
    public float attackCD;
    public float endurance;
    public float recoverEndurance;
    public float stunDuration;
    public float speed;
    public string description;
    public EnemySkillSO enemySkill;
    public List<GameObject> spawnPrefab = new List<GameObject>();
    public int maxSpawnAmount;
    public bool flyingUnit;
}
