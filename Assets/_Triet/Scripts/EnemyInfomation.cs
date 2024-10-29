using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfomation : MonoBehaviour
{
    public string enemyName { get; private set; }
    [Header("status")]
    public EnemyRace race;
    [SerializeField] private float enemyMaxHp;
    [SerializeField] private float enemyCurrentHp;
    [SerializeField] private float enemyAtk;
    [SerializeField] private float enemyAtkSpeed;
    [SerializeField] private float enemyDef;
    [SerializeField] private float enemyCriticalRate;
    [SerializeField] private float enemyCriticalDmg;
    [Header("Slider")]
    [SerializeField] private Slider sliderHpBar;
    public EnemyStateMachine stateMachine;
    [SerializeField] private GameObject enemyUI;
    public bool isWork;

    private void Start()
    {
        GetEnemyData();
        sliderHpBar.maxValue = enemyMaxHp;
        sliderHpBar.value = enemyCurrentHp;
    }
    IEnumerator ChangedHpBar(float hp)
    {
        var temp = enemyCurrentHp;
        while(temp > hp)
        {
            yield return new WaitForSeconds(0.001f);
            temp -= 1f;
            sliderHpBar.value = temp;
        }
    }
    private void GetEnemyData()
    {
        var enemyInfo = stateMachine.spawner.enemyData;
        enemyMaxHp = enemyInfo.basicHp;
        enemyCurrentHp = enemyInfo.basicHp;
        enemyAtk = enemyInfo.basicAtk;
        enemyAtkSpeed = enemyInfo.basicAtkSpd;
        enemyDef = enemyInfo.basicDef;
        enemyCriticalRate = enemyInfo.basicCriticalRate;
        enemyCriticalDmg = enemyInfo.basicCriticalDmg;
    }
    private void LateUpdate()
    {
        enemyUI.transform.LookAt(stateMachine.spawner.playerPosition);
        enemyUI.transform.Rotate(new Vector3(0,180,0));


        if (Input.GetKeyDown(KeyCode.Backspace) && isWork)
        {
            EnemyTakeDame(100);
        }
    }
    public void EnemyTakeDame(float damage)
    {
        bool isDied = enemyCurrentHp - damage <= 0;
        if (!isDied)
        {
            StartCoroutine(ChangedHpBar(enemyCurrentHp - damage));
            enemyCurrentHp -= damage;
        }
        else
        {
            Debug.Log("Enemy died");
            stateMachine.ChangeState(EnemyState.Dead);
        }
    }
}

public enum EnemyRace
{
    None,
    Undead,
    Dragon,
    Devil,
    Wolf
}

public enum EnemyRarity
{
    Normal,
    Elite,
    Rare,
    Epic,
    Legendary
}
