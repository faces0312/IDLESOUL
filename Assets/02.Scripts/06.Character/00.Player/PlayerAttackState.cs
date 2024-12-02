using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    protected float attackStateMoveModifter = 0.0f;


    public PlayerAttackState(PlayerStateMachine _stateMachine) : base(_stateMachine)
    {
       
    }

    public override void Enter()
    {
        moveSpeedModifier = attackStateMoveModifter;
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
    }
}
