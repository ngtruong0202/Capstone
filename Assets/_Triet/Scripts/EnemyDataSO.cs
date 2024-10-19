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
    public string enemyRaceName;
    public EnemyRace race;
    public EnemyRarity maxRarity;
    public float basicHp;
    public float basicAtk;
    public float basicAtkSpd;
    public float basicDef;
    public float basicCriticalRate;
    public float basicCriticalDmg;
    public string description;
    public EnemySkillSO enemySkill;
    public List<GameObject> spawnPrefab = new List<GameObject>();
    public int maxSpawnAmount;
}
