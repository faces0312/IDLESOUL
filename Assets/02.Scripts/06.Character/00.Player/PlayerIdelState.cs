using System.Diagnostics;

public class PlayerIdelState : PlayerBaseState
{   
    public PlayerIdelState(PlayerStateMachine _stateMachine) : base(_stateMachine)
    {
        stateMachine = _stateMachine;
    }

    public override void Enter()
    { 
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        base.Update();

        //적이 있는지 탐색함
        stateMachine._Player.targetSearch.OnTargetSearch();

        if(stateMachine._Player.targetSearch.ShortEnemyTarget != null)
        {
            //이동 상태로 전환
            stateMachine.ChangeState(stateMachine.MoveState);
        }


        ////공격 상태로 전환
        //stateMachine.ChangeState(stateMachine.AttackState);
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
