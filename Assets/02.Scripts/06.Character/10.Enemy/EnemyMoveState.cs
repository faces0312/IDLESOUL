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
        StartAnimation(animatorHashData.WalkParameterHash);
        StopAnimation(animatorHashData.IdleParameterHash);
        /*StopAnimation(animatorHashData.IdleParameterHash);
        StopAnimation(animatorHashData.AttackParameterHash);
        StartAnimation(animatorHashData.WalkParameterHash);*/
        /*StartAnimation(animatorHashData.WalkParameterHash);
        StopAnimation(animatorHashData.IdleParameterHash);*/
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
        if (distanceTmp > 2f)//stateMachine.Enemy.enemyDB.Distance)
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
