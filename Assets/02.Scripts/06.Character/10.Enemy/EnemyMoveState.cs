using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyBaseState
{
    private Vector3 direction;
    public EnemyMoveState(EnemyStateMachine _stateMachine) : base(_stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        /*animator.SetBool(animatorHashData.IdleParameterHash, false);
        animator.SetBool(animatorHashData.WalkParameterHash, true);
        animator.SetTrigger(animatorHashData.AttackParameterHash, false);*/
        Debug.Log("움직임 애니메이션");
        // 이동 애니메이션 시작
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }

    void Move()
    {
        direction = (stateMachine.Enemy.target.transform.position - stateMachine.Enemy.transform.position).normalized;
        float distanceTmp = Vector3.Distance(stateMachine.Enemy.transform.position, stateMachine.Enemy.target.transform.position);
        if (distanceTmp > stateMachine.Enemy.enemyDB.Distance)
        {
            Vector3 targetVelocity = direction * stateMachine.Enemy.enemyDB.MoveSpeed;
            stateMachine.Enemy.rb.velocity = targetVelocity;
        }
        else
        {
            stateMachine.Enemy.rb.velocity = Vector3.zero;
            stateMachine.ChangeState(stateMachine.AttackState);
        }
    }
}
