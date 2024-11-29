using UnityEngine;

public abstract class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;


    public PlayerBaseState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void Update()
    {
        Move();
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

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();

        Move(movementDirection);
    }

    private Vector3 GetMovementDirection()
    {

        if (stateMachine._Player.targetSearch.ShortEnemyTarget != null)
        {
            Vector3 TargetPos = stateMachine._Player.targetSearch.ShortEnemyTarget.transform.position;
            Vector3 targetDir = (TargetPos - stateMachine._Player.transform.position).normalized;
            return targetDir;
        }
        else
        {
            return Vector3.zero;
        }

    }

    private void Move(Vector3 direction)
    {
        //float movementSpeed = stateMachine._Player.StatHandler.CurrentStat.moveSpeed;
        float movementSpeed = stateMachine._Player.UserData.stat.moveSpeed;
        //캐릭터컨트롤러 컴포넌트에는 Move라는 내부 메서드가 기본적으로 생성되어있음
        stateMachine._Player.rb.velocity = ((direction * movementSpeed));
    }

}
