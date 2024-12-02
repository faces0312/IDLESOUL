using UnityEngine;

public class PlayerMeleeAttackState : PlayerAttackState
{
    public float defaultAttackRange = 3.0f;

    public PlayerMeleeAttackState(PlayerStateMachine _stateMachine) : base(_stateMachine)
    {
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Player Melee Attack State Enter");
        string animName = stateMachine._Player.PlayerAnimationController.MeleeAttackAnimationName;
        stateMachine._Player.PlayerAnimationController.spineAnimationState.SetAnimation(0, animName, true);
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Player Melee Attack State Exit");
    }

    public override void Update()
    {
        base.Update();

        //추격할 타겟이 있으면 공격 진행 
        if (stateMachine._Player.targetSearch.ShortEnemyTarget != null)
        {
            Debug.Log("Player Attack Start");
        }
        else //타겟이 없어지면 대기상태로 전환
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    public override void FixedUpdate()
    {
    }

}
