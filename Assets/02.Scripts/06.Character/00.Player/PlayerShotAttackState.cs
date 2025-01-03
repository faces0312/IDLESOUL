using System.Collections;
using UnityEngine;

public class PlayerShotAttackState : PlayerAttackState
{
    public float defaultAttackRange = 10.0f;
    private float currentAttackTimer = 0f;

    private int atkSFXCurCount = 0; // 현재 공격한 횟수  
    private int atkSFXCount = 4; // 기본 평타 공격 소리가 시끄러워서 해당 횟수 공격이후 효과음 출력하는 변수 

    public PlayerShotAttackState(PlayerStateMachine _stateMachine) : base(_stateMachine)
    {
        stateMachine = _stateMachine;
        atkSFXCurCount = atkSFXCount;
    }

    public override void Enter()
    {
        base.Enter();

        //Debug.Log("Player Shot Attack State Enter");
        string animName = stateMachine._Player.PlayerAnimationController.ShotAttackAnimationName;
        currentAttackTimer = 0;//stateMachine._Player.UserData.stat.atkSpeed;
    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("Player Shot Attack State Exit");
    }

    public override void Update()
    {
        base.Update();

        //추격할 타겟이 있으면 공격 진행 
        if (stateMachine._Player.targetSearch.ShortEnemyTarget != null)
        {
            //Debug.Log("Player Shot Attack Start");

            if (stateMachine._Player.UserData.stat.atkSpeed <= currentAttackTimer)
            {
                if (atkSFXCurCount >= atkSFXCount)
                {
                    stateMachine._Player.PlayerSFX.PlayClipSFXOneShot((SoundType)Random.Range(0, 2));
                    atkSFXCurCount--;
                }
                else if(atkSFXCount <= 0)
                {
                    atkSFXCurCount = atkSFXCount;
                }
               

                string animName = stateMachine._Player.PlayerAnimationController.ShotAttackAnimationName;
                stateMachine._Player.PlayerAnimationController.spineAnimationState.SetAnimation(0, animName, false);

                GameObject obj = ObjectPoolManager.Instance.GetPool(Const.PLAYER_PROJECTILE_ENERGYBOLT_KEY, Const.POOL_KEY_PLAYERPROJECTILE).GetObject();
                Vector3 TargetPos = stateMachine._Player.targetSearch.ShortEnemyTarget.transform.position;
                Vector3 targetDir = (TargetPos - stateMachine._Player.transform.position).normalized;
                targetDir.y = 0f; //y축 보정
                obj.transform.position = stateMachine._Player.transform.position;
                obj.GetComponent<PlayerProjectile>().dir = targetDir;
                obj.SetActive(true);
                currentAttackTimer = 0f;
            }
            else
            {

                currentAttackTimer += Time.deltaTime * 2;
            }

        }
        else //타겟이 없어지면 대기상태로 전환
        {
            atkSFXCurCount = atkSFXCount;
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    public override void FixedUpdate()
    {
    }

}
