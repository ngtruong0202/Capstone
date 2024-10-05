using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName { get; private set; }
    public EnemyRace race;
    public float enemyMaxHp;
    public float enemyCurrentHp;
    public float enemyAtk;
    public float enemyAtkSpeed;
    public float enemyDef;
    public float enemyCriticalRate;
    public float enemyCriticalDmg;

    private void GetEnemyData()
    {
        var enemyInfo = EnemyManager.Instance.enemyDataSO.datas.Find(data => data.race == race);
        if(enemyInfo.maxType >= EnemyType.Lord)
        {
            Debug.Log($"{enemyInfo.maxRarity} {enemyInfo.enemyRaceName} {enemyInfo.maxType} Abc");
        }
    }

    private void InitEnemyData()
    {
        int[] multi = new int[5] { 1, 2, 5, 7, 9 };

    }

    private void Start()
    {
        GetEnemyData();
    }

}

public enum EnemyRace
{
    None,
    Undead,
    Dragon,
    Devil
}

public enum EnemyRarity
{
    Normal,
    Elite,
    Rare,
    Epic,
    Legendary
}

public enum EnemyType
{
    Soldier,
    General,
    Lord,
    King,
    Emperor
}
