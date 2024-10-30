using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private EnemyDetection enemyDetection;

    private int currentAttack;
    private float cooldowntime = 1;
    private float timeAttack;
    private float enemyDistance;
    private float quickAttackDeltaDistance;


    void Update()
    {
        timeAttack += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if (enemyDetection.currentTarget == null)
                Attack();
            else
            {
                MoveTowardsTarget("MMAKick",enemyDetection.currentTarget.position, quickAttackDeltaDistance);
            }
            
        }
    }

    public void Attack()
    {
        if(timeAttack >= 0.7f)
        {
            currentAttack++;

            if (timeAttack > 2f)
                currentAttack = 1;

            if (currentAttack > 4) return;
            animator.SetTrigger("Attack" + currentAttack);
            timeAttack = 0;
        }
        else if(currentAttack == 4 && timeAttack > 1)
        {
            animator.SetTrigger("Attack4");
            currentAttack = 0;
            timeAttack = 0;
        }
    }

    public void MoveTowardsTarget(string animationName, Vector3 enemyPos, float deltaDistance)
    {
        PerformAttackAnimation(animationName);
        FaceThis(enemyPos);

        Vector3 finalPos = TargetOffset(enemyPos, deltaDistance);
        finalPos.y = 0;

        StartCoroutine(Move(enemyPos));
    }

    private void PerformAttackAnimation(string animationName)
    {
        animator.SetTrigger(animationName);
    }

    public void FaceThis(Vector3 target)
    {

        Vector3 direction = new Vector3(target.x - transform.position.x, 0, target.z - transform.position.z);

        Quaternion lookAtRotation = Quaternion.LookRotation(direction);

        rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, lookAtRotation, 200 * Time.deltaTime));
    }

    public Vector3 TargetOffset(Vector3 target, float deltaDistance)
    {
        return Vector3.MoveTowards(target, transform.position, deltaDistance);
    }

    IEnumerator Move(Vector3 targetPosition)
    {
        while (Vector3.Distance(rb.position, targetPosition) > 0.5f)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPosition, 5 * Time.deltaTime);
            rb.MovePosition(newPosition);
            yield return null;
        }
    }
}
