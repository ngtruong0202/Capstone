using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Truong
{
    public class PlayerAttackCombo3State : PlayerEventAttactState
    {
        public PlayerAttackCombo3State(PlayerAttackStateMachine playerAttackState) : base(playerAttackState)
        {
        }

        public override void Enter()
        {
            base.Enter();

            ResetAttackState();
            startTime = Time.time;

            stateMachine.Player.Rigidbody.transform.position += stateMachine.Player.Rigidbody.transform.forward * 3f;

            if(stateMachine.Player.EnemyDetection.currentTarget != null)
            {
                RotateTowardsTarget(stateMachine.Player.EnemyDetection.currentTarget.position);
            }
            

            StartAnimation(stateMachine.Player.PlayerAnimation.attack, 3);

            Attack3();

        }

        public void Attack3()
        {
            GameObject obj = PoolManager.Instance.GetPooledObject("Attack3");
            if (obj != null)
            {
                Vector3 pos = new Vector3(stateMachine.Player.transform.position.x, 
                    stateMachine.Player.transform.position.y - 1, stateMachine.Player.transform.position.z);

                obj.transform.position = pos;
                obj.transform.rotation = stateMachine.Player.transform.rotation;
                obj.SetActive(true);
            }
        }
    }

}

