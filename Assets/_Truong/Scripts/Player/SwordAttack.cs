using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private int currentAttack;
    private float cooldowntime = 1;
    private float timeAttack;

    void Start()
    {
        
    }

    
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

  
}
