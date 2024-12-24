using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    //ToDO : 나중에 Json 으로 데이터 뺴야됨
    private static readonly float AttackRange = 5.0f;
    private float moveStateMoveModifter = 1.0f;
    public PlayerMoveState(PlayerStateMachine _stateMachine) : base(_stateMachine)
    {
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        //Debug.Log("Player Move State Enter");
        string animName = stateMachine._Player.PlayerAnimationController.runAnimationName;
        stateMachine._Player.PlayerAnimationController.spineAnimationState.SetAnimation(0, animName, true);

        moveSpeedModifier = moveStateMoveModifter;
    }

    public override void Exit()
    {
        //Debug.Log("Player Move State Exit");
    }

    public override void Update()
    {
        base.Update();

        //추격할 타겟이 사라지면 대기 상태로 전환
        if (stateMachine._Player.targetSearch.ShortEnemyTarget != null)
        {
            float targetDist = Vector3.Distance(stateMachine._Player.transform.position, stateMachine._Player.targetSearch.ShortEnemyTarget.transform.position);

            //적과의 거리에 따라 기본 공격 
            if (stateMachine._Player.DefaultAttackType && targetDist <= stateMachine.MeleeAttackState.defaultAttackRange)
            {
                //근접 공격 상태로 전환
                stateMachine.ChangeState(stateMachine.IdleState);
            }
            else if (!stateMachine._Player.DefaultAttackType && targetDist <= stateMachine.ShotAttackState.defaultAttackRange)
            {
                //원거리 공격 상태로 전환
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
        else //추격할 타겟이 사라지면 대기 상태로 전환
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    public override void FixedUpdate()
    {
    }

    protected void StartAnimation(int animatorHash)
    {
        //stateMachine.{객체}.Animator.SetBool(animatorHash, true);
    }

    protected void StartAnimationTrigger(int animatorHash)
    {
        //stateMachine.{객체}r.Animator.SetTrigger(animatorHash);
    }

    protected void StopAnimation(int animatorHash)
    {
        //stateMachine.{객체}.Animator.SetBool(animatorHash, false);
    }


}
