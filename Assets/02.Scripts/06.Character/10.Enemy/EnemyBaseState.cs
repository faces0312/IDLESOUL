using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemy;
    //protected Animator animator;
    protected AnimatorHashData animatorHashData;
    public EnemyBaseState(EnemyStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        this.enemy = stateMachine.Enemy;
        //this.animator = enemy.animator;
        this.animatorHashData = enemy.animatorHashData;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
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
