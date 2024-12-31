using UnityEngine;

public class PlayerIdelState : PlayerBaseState
{
    //ToDO : 나중에 Json 으로 데이터 뺴야됨
    private static readonly float AttackRange = 5.0f;
    private float idleStateMoveModifter = 0.0f;

    public PlayerIdelState(PlayerStateMachine _stateMachine) : base(_stateMachine)
    {
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        string animName = stateMachine._Player.PlayerAnimationController.idleAnimationName;
        stateMachine._Player.PlayerAnimationController.spineAnimationState.SetAnimation(0, animName, true);

        moveSpeedModifier = idleStateMoveModifter;
    }

    public override void Exit()
    {
        //Debug.Log("Player Idle State Exit");
    }

    public override void Update()
    {
        base.Update();

        //적이 있는지 탐색함
        stateMachine._Player.targetSearch.OnTargetSearch();

        if(stateMachine._Player.targetSearch.ShortEnemyTarget != null)
        {
            float targetDist = Vector3.Distance(stateMachine._Player.transform.position, stateMachine._Player.targetSearch.ShortEnemyTarget.transform.position);

            //적과의 거리에 따라 기본 공격 
            if (stateMachine._Player.DefaultAttackType && targetDist <= stateMachine.MeleeAttackState.defaultAttackRange)
            {
                //근접 공격 상태로 전환
                stateMachine.ChangeState(stateMachine.MeleeAttackState);
            }
            else if (!stateMachine._Player.DefaultAttackType && targetDist <= stateMachine.ShotAttackState.defaultAttackRange)
            {
                //원거리 공격 상태로 전환
                stateMachine.ChangeState(stateMachine.ShotAttackState);
            }
            else if(stateMachine._Player.isAuto == true)//공격 범위가 아닌경우 적에게 이동  
            {
                //이동 상태로 전환
                stateMachine.ChangeState(stateMachine.MoveState);
            }
        }      
    }


}
