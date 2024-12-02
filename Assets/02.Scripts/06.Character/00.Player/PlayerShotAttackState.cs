using UnityEngine;

public class PlayerShotAttackState : PlayerAttackState
{
    public PlayerShotAttackState(PlayerStateMachine _stateMachine) : base(_stateMachine)
    {
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        Debug.Log("Player Melee Attack State Enter");
        string animName = stateMachine._Player.PlayerAnimationController.AttackAnimationName;
        stateMachine._Player.PlayerAnimationController.spineAnimationState.SetAnimation(0, animName, true);

        moveSpeedModifier = attackStateMoveModifter;
        defaultAttackRange = 2.5f;
    }

    public override void Exit()
    {
        Debug.Log("Player Melee Attack State Exit");
    }

    public override void Update()
    {
        base.Update();

        //추격할 타겟이 있으면 공격 진행 
        if (stateMachine._Player.targetSearch.ShortEnemyTarget != null)
        {
            Debug.Log("Player Melee Attack Start");
        }
        else //타겟이 없어지면 대기상태로 전환
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
