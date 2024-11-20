using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Truong
{
    public class PlayerAttackState : IState
    {
        protected PlayerAttackStateMachine stateMachine;
        protected PlayerGroundedData movementData;

        public int currentAttack;
        public float cooldowntime;
        public float timeAttack;
        public float startTime;
        public float cooldownTime = 1;



        public PlayerAttackState(PlayerAttackStateMachine playerAttackStateMachine)
        {
            stateMachine = playerAttackStateMachine;

            movementData = stateMachine.Player.Data.GroundedData;
        }

        public virtual void Enter()
        {
            Debug.Log("State : " + GetType().Name);

            AddInputActionCallbacks();

        }

        public virtual void Exit()
        {
             RemoveInputActionCallbacks();
        }

        public virtual void HandleInput()
        {
           
        }

        public virtual void PhysicUpdate()
        {
          
        }

        public virtual void UpDate()
        {
            
        }

        public void ResetState()
        {
            stateMachine.Player.Rigidbody.velocity = Vector3.zero;

            stateMachine.Player.Inputs.playerActions.Movement.Disable();

            stateMachine.Player.Inputs.DisableActionFor(stateMachine.Player.Inputs.playerActions.Attack,
                stateMachine.Player.Data.AttackData.CooldownTime);

        }

        public void BasicAttacks()
        {
            if(Time.time > startTime + stateMachine.Player.Data.AttackData.ResetAttackTime)
            {
                currentAttack = 1;
            }
            else
            {
                currentAttack++;

                if (currentAttack == 3)
                {

                    stateMachine.ChangeState(stateMachine.PlayerAttackCombo3);
                    return;
                }
                else if (currentAttack == 4)
                {

                    stateMachine.ChangeState(stateMachine.PlayerAttackCombo4);
                    return;
                }
                else if (currentAttack > 4)
                {
                    currentAttack = 1;
                }

            }
            startTime = Time.time;
            StartAnimation(stateMachine.Player.PlayerAnimation.attack, currentAttack);

        }
 

        public void MoveTowardsTarget(string animationName, Vector3 enemyPos, float deltaDistance)
        {
            RotateTowardsTarget(enemyPos);

            stateMachine.Player.Animator.SetTrigger(animationName);

            Vector3 finalPos = Vector3.MoveTowards(enemyPos, stateMachine.Player.transform.position, deltaDistance);
            finalPos.y = 0;

            Move(enemyPos);
        }

        public void RotateTowardsTarget(Vector3 targetPosition)
        {      
            Vector3 direction = (targetPosition - stateMachine.Player.Rigidbody.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            stateMachine.Player.Agent.transform.rotation = lookRotation;
        }

        public IEnumerator Move(Vector3 targetPosition)
        {
           
            while (Vector3.Distance(stateMachine.Player.Rigidbody.position, targetPosition) > 2f)
            {
                Vector3 newPosition = Vector3.MoveTowards(stateMachine.Player.Rigidbody.position, targetPosition, 4 * Time.deltaTime);
                stateMachine.Player.Rigidbody.MovePosition(newPosition);
                yield return null;
            }
            
        }

        public IEnumerator EnemyDistance()
        {
            if (!stateMachine.Player.Agent.enabled)
            {
                stateMachine.Player.Agent.enabled = true;
            }

            if (stateMachine.Player.EnemyDetection.currentTarget == null)
            {
                yield break; 
            }

            while (stateMachine.Player.EnemyDetection.currentTarget != null)
            {
                

                float enemyDistance = Vector3.Distance(stateMachine.Player.Agent.transform.position, stateMachine.Player.EnemyDetection.currentTarget.position);

                if (enemyDistance < stateMachine.Player.Agent.stoppingDistance)
                {
                    
                    if (stateMachine.Player.Agent.enabled)
                    {
                        stateMachine.Player.Agent.isStopped = true;
                        stateMachine.Player.Agent.enabled = false; 
                    }
 
                    stateMachine.Player.Animator.SetBool(stateMachine.Player.PlayerAnimation.Running, false);

                    stateMachine.ChangeState(stateMachine.BasicAttackState);

                    yield break;
                }
                else
                {
                    if(!stateMachine.Player.Agent.enabled)
                    {
                        yield break;
                    }

                    stateMachine.Player.Agent.isStopped = false;
                    stateMachine.Player.Agent.SetDestination(stateMachine.Player.EnemyDetection.currentTarget.position);

                    stateMachine.Player.Animator.SetBool(stateMachine.Player.PlayerAnimation.Running, true);
                }

                yield return null;
            }
        }

        protected void StartAnimation(string animationHash, int currentAttack)
        {
            stateMachine.Player.Animator.SetTrigger(animationHash + currentAttack);
        }

        protected virtual void AddInputActionCallbacks()
        {
            
        }

        protected virtual void RemoveInputActionCallbacks()
        {

        }

        public virtual void OnAnimationEnterEvent()
        {

        }

        public virtual void OnAnimationExitEvent()
        {

        }

        public virtual void OnAnimationTransitionEvent()
        {

        }
    }
}
