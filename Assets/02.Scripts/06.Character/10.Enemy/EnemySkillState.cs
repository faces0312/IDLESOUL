using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySkillState : EnemyBaseState
{
    public BossEnemy bossEnemy; 
    private int currentSkillIndex = 0;
    private EnemySkill1 bossSkill1; // EnemySkill1 인스턴스를 저장할 필드

    public EnemySkillState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        bossEnemy = stateMachine.Enemy as BossEnemy;
        bossSkill1 = new EnemySkill1(bossEnemy, stateMachine);
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Enemy.rb.velocity = Vector3.zero;
        StopAnimation(animatorHashData.WalkParameterHash);
        StartAnimationTrigger(animatorHashData.SkillParameterHash);
    }

    public IEnumerator PerformSkill()
    {
        //보스가 스킬을 가지고 있다면 스킬 발동
        if (bossEnemy.skill.Count > 0)
        {
            yield return bossEnemy.skill[currentSkillIndex].PerformSkill();
            //다음 스킬 인덱스 이동.
            currentSkillIndex = (currentSkillIndex + 1) % bossEnemy.skill.Count;
        }
        else//스킬이 없다면 이동 상태로 전환
        {
            stateMachine.ChangeState(stateMachine.MoveState);
        }
    }

    public void MeleeSkillBossStart()
    {
        if (stateMachine.Enemy.transform.localScale.x > 0)
            stateMachine.AttackState.meleeAttack = EnemyManager.Instance.EnemyAttackSpawn(6002, new Vector3(stateMachine.Enemy.transform.position.x - 0.5f, stateMachine.Enemy.transform.position.y, stateMachine.Enemy.transform.position.z), Quaternion.Euler(90, 0, 90));
        else
            stateMachine.AttackState.meleeAttack = EnemyManager.Instance.EnemyAttackSpawn(6002, new Vector3(stateMachine.Enemy.transform.position.x + 0.5f, stateMachine.Enemy.transform.position.y, stateMachine.Enemy.transform.position.z), Quaternion.Euler(90, 180, 90));

        bossEnemy.skillChargingEffect.SetActive(false);
        foreach (Transform child in bossEnemy.skillZone.transform)
            child.gameObject.SetActive(false);

        bossSkill1.SkillAttack1();
    }
}
