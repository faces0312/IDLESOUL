using Spine.Unity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public abstract class PlayerBaseState : IState
{
    protected float moveSpeedModifier = 1.0f;

    protected PlayerStateMachine stateMachine;

    private bool isBorder = false;

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
        //자동모드일때
        if (stateMachine._Player.isAuto)
        {
            Vector3 movementDirection = GetMovementDirection();

            Move(movementDirection);
        }
    }

    public void FlipCharacter(bool isFacingRight)
    {
        // true이면 ScaleX를 1로 설정, false이면 -1로 설정
        stateMachine._Player.PlayerAnimationController.skeleton.ScaleX = isFacingRight ? 1f : -1f;
    }

    private Vector3 GetMovementDirection()
    {

        if (stateMachine._Player.targetSearch.ShortEnemyTarget != null)
        {
            Vector3 TargetPos = stateMachine._Player.targetSearch.ShortEnemyTarget.transform.position;
            Vector3 targetDir = (TargetPos - stateMachine._Player.transform.position).normalized;
            targetDir.y = 0; //y축 데이터 보정

            if (targetDir.x > 0)
            {
                FlipCharacter(true);
            }
            else
            {
                FlipCharacter(false);
            }

            return targetDir;
        }
        else
        {
            return Vector3.zero;
        }

    }

    private void Move(Vector3 direction)
    {
        float movementSpeed = stateMachine._Player.UserData.stat.moveSpeed;
        //캐릭터컨트롤러 컴포넌트에는 Move라는 내부 메서드가 기본적으로 생성되어있음
        stateMachine._Player.rb.velocity = ((direction * movementSpeed)) * moveSpeedModifier;
    }

}
