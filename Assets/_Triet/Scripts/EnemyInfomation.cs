using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfomation : MonoBehaviour
{
    public string enemyName { get; private set; }
    public float EnemySpeed { get => enemySpeed; set => enemySpeed = value; }
    public bool FlyingUnit { get => flyingUnit; set => flyingUnit = value; }

    [Header("status")]
    public EnemyRace race;
    [SerializeField] private float enemyMaxHp;
    [SerializeField] private float enemyCurrentHp;
    [SerializeField] private float enemyAtk;
    [SerializeField] private float enemyAtkCd;
    [SerializeField] private float enemyDef;
    [SerializeField] private float enemyEndurance;
    [SerializeField] private float enemyRecoverEndurance;
    [SerializeField] private float enemyStunDuration;
    [SerializeField] private float enemySpeed;
    [SerializeField] private bool flyingUnit;
    [Header("Slider")]
    [SerializeField] private Slider sliderHpBar;
    public EnemyStateMachine stateMachine;
    [SerializeField] private GameObject enemyUI;
    public bool isWork;
    [Header("Area")]
    public float warningArea;
    public float chaseArea;
    public float attackArea;

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
        enemyMaxHp = enemyInfo.hp;
        enemyCurrentHp = enemyInfo.hp;
        enemyAtk = enemyInfo.attack;
        enemyAtkCd = enemyInfo.attackCD;
        enemyDef = enemyInfo.defense;
        enemyEndurance = enemyInfo.endurance;
        enemyRecoverEndurance = enemyInfo.recoverEndurance;
        EnemySpeed = enemyInfo.speed;

        FlyingUnit = enemyInfo.flyingUnit;
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
            stateMachine.isDead = true;
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
