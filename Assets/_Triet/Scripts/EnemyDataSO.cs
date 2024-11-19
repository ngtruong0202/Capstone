using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data Manager", menuName = "Enemy/EnemyDataManager")]
public class EnemyDataSO : ScriptableObject
{
    public List<EnemyData> datas = new List<EnemyData>();
}

