public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine _stateMachine) : base(_stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        /*animator.SetBool(animatorHashData.IdleParameterHash, true);
        animator.SetBool(animatorHashData.WalkParameterHash, false);
        animator.SetTrigger(animatorHashData.AttackParameterHash, false);*/
        //StartAnimation(stateMachine.SlimeTower.AnimatorHashData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        //StopAnimation(stateMachine.SlimeTower.AnimatorHashData.IdleParameterHash);
    }

    public override void Update()
    {
       
    }


    public override void FixedUpdate()
    {
    }
}
