using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private EnemyDetection enemyDetection;

    private int currentAttack;
    private float cooldowntime = 1;
    private float timeAttack;
    
    [Tooltip("Offset Stoping Distance")][SerializeField] private float quickAttackDeltaDistance;


    void Update()
    {

        timeAttack += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            Attack();           
        }
        
    }

    public void Attack()
    {
        if (enemyDetection.currentTarget == null)
        {  
            if(enemyDetection.detecedEnemies.Count == 0)
            {
                BasicAttack();
            }
               
            else
            {
                enemyDetection.GetClosestEnemy();
                StartCoroutine(EnemyDistance());
            }
                
        }       
        else
        {
            float enemyDistance = Vector3.Distance(agent.transform.position, enemyDetection.currentTarget.transform.position);

            if(enemyDistance <= agent.stoppingDistance)
            {
                agent.updateRotation = false;
                RotateTowardsTarget(enemyDetection.currentTarget.position);
                agent.updateRotation = true;
                BasicAttack();
            }
            else
            {
                StartCoroutine(EnemyDistance());                
            }         
        }
    }

    public void BasicAttack()
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
        RotateTowardsTarget(enemyPos);
        PerformAttackAnimation(animationName);

        Vector3 finalPos = TargetOffset(enemyPos, deltaDistance);
        finalPos.y = 0;

        StartCoroutine(Move(enemyPos));
    }

    private void PerformAttackAnimation(string animationName)
    {
        animator.SetTrigger(animationName);
    }

    void RotateTowardsTarget(Vector3 targetPosition)
    {
        //rb.transform.LookAt(targetPosition);
        Vector3 direction = (targetPosition - rb.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        agent.transform.rotation = lookRotation;

    }



    public Vector3 TargetOffset(Vector3 target, float deltaDistance)
    {
        return Vector3.MoveTowards(target, transform.position, deltaDistance);
    }

    IEnumerator Move(Vector3 targetPosition)
    {
        agent.enabled = false;
        while (Vector3.Distance(rb.position, targetPosition) > 2f)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPosition, 4 * Time.deltaTime);
            rb.MovePosition(newPosition);
            yield return null;
        }
        agent.enabled = true;
    }

    IEnumerator EnemyDistance()
    {
        if (enemyDetection.currentTarget == null)
        {
            yield break;
        }

        while (enemyDetection.currentTarget != null)
        {  
            float enemyDistance = Vector3.Distance(agent.transform.position, enemyDetection.currentTarget.position);

            if(enemyDistance < agent.stoppingDistance)
            {
                agent.isStopped = true;
                BasicAttack();
                yield break;
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(enemyDetection.currentTarget.position);
            }

            yield return null;
        }
    }

}
