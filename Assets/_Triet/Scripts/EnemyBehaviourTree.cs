using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourTree : MonoBehaviour
{
    [Space]
    [SerializeField] private EnemyStateMachine stateMachine;
    [SerializeField] private EnemyInfomation enemyInfomation;
    [SerializeField] float timerAttack;

    private Animator animator;
    bool attacking;
    private int isCombo;
    private int comboCount = 1;
    public int currentComboList;
    private bool completedAtk;
    PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        timerAttack = enemyInfomation.EnemyAtkCd;
        completedAtk = true;
        animator = stateMachine.animator;
        currentComboList = stateMachine.spawner.GetEnemyComboList();
        playerHealth = stateMachine.spawner.playerPosition.gameObject.GetComponent<PlayerHealth>();
    }
    public void CountdownTimer()
    {
        timerAttack += Time.deltaTime;
    }
    public bool CanAttack(float timeChange)
    {
        return timerAttack >= timeChange;
    }
    public bool CompletedPreAtk()
    {
        return completedAtk;
    }
    public void EnemyAttack()
    {
        timerAttack = 0;
        attacking = true;
        SetAnimValue("isAttack", attacking);
        StartCoroutine(WaitingAttack());
    }
    IEnumerator WaitingAttack()
    {
        Debug.LogError("enter attack");
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);
        Debug.LogError("waiting time: " + stateInfo.length);
        playerHealth.PlayerTakeDamage(enemyInfomation.EnemyAtk);
        while(comboCount < currentComboList)
        {
            Debug.LogError($"{comboCount}/{currentComboList}, {comboCount < currentComboList}");
            comboCount++;
            SetAnimValue("Combo", comboCount);
            SetAnimValue("isCombo", true);
            stateInfo = animator.GetCurrentAnimatorStateInfo(0); 
            yield return new WaitForSeconds(stateInfo.length);
            playerHealth.PlayerTakeDamage(enemyInfomation.EnemyAtk);
        }
        comboCount = 1;
        SetAnimValue("isAttack", false);
        SetAnimValue("isCombo", false);
        yield return null;
    }
    public void CheckEnemyHealth()
    {
        if (!enemyInfomation.CheckEnemyHealth()) return;
        ActiveSpecialAttack();
    }
    private void ActiveSpecialAttack()
    {
        currentComboList += stateMachine.spawner.GetEnemyWaitingComboList();
    }
    public void ExitAttackState()
    {
        attacking = false;
        SetAnimValue("isAttack", attacking);
    }
    private void SetAnimValue(string name, bool value)
    {
        animator.SetBool(name, value);
    }
    private void SetAnimValue(string name, int value)
    {
        animator.SetInteger(name, value);
    }
}
