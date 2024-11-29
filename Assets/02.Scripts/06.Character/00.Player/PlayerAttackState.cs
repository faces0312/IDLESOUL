
public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine _stateMachine) : base(_stateMachine)
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
