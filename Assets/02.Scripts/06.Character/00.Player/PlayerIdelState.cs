using UnityEngine;

public class PlayerIdelState : PlayerBaseState
{
    //ToDO : 나중에 Json 으로 데이터 뺴야됨
    private static readonly float AttackRange = 5.0f;


    public PlayerIdelState(PlayerStateMachine _stateMachine) : base(_stateMachine)
    {
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        Debug.Log("Player Idle State Enter");
        string animName = stateMachine._Player.PlayerAnimationController.idleAnimationName;
        stateMachine._Player.PlayerAnimationController.spineAnimationState.SetAnimation(0, animName, true);
    }

    public override void Exit()
    {
        Debug.Log("Player Idle State Exit");
    }

    public override void Update()
    {
        base.Update();

        //적이 있는지 탐색함
        stateMachine._Player.targetSearch.OnTargetSearch();

        if(stateMachine._Player.targetSearch.ShortEnemyTarget != null)
        {
            float targetDist = Vector3.Distance(stateMachine._Player.transform.position, stateMachine._Player.targetSearch.ShortEnemyTarget.transform.position);

            //적과의 거리가 공격범위보다 작으면 공격 상태로 진입
            if (targetDist <= AttackRange)
            {
                //공격 상태로 전환
                stateMachine.ChangeState(stateMachine.AttackState);
            }
            else //공격 범위가 아닌경우 적에게 이동  
            {
                //이동 상태로 전환
                stateMachine.ChangeState(stateMachine.MoveState);
            }
          
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
