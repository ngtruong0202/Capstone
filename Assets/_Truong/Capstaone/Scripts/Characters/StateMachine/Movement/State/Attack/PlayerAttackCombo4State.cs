using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Truong
{
    public class PlayerAttackCombo4State : PlayerEventAttactState
    {
        public PlayerAttackCombo4State(PlayerAttackStateMachine playerAttackState) : base(playerAttackState)
        {
        }

        public override void Enter()
        {
            base.Enter();

            ResetState();

            stateMachine.Player.Rigidbody.transform.position += stateMachine.Player.Rigidbody.transform.forward * 3f;

            if (stateMachine.Player.EnemyDetection.currentTarget != null)
            {
                RotateTowardsTarget(stateMachine.Player.EnemyDetection.currentTarget.position);
            }

            StartAnimation(stateMachine.Player.PlayerAnimation.attack, 4);


        }

        public void Attack4(Transform position)
        {
            GameObject obj = PoolManager.Instance.GetPooledObject("Attack4");
            if (obj != null)
            {
                Vector3 pos = new Vector3(stateMachine.Player.transform.position.x + 1,
                    stateMachine.Player.transform.position.y + 1.5f, stateMachine.Player.transform.position.z - 1f);

                obj.transform.position = position.position;
                obj.transform.rotation = stateMachine.Player.transform.rotation;
                obj.SetActive(true);
            }
        }
    }
}

