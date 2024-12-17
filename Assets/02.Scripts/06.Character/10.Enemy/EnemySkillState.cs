using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySkillState : EnemyBaseState
{
    public BossEnemy bossEnemy; 
    private int currentSkillIndex = 0;
    private EnemySkill1 bossSkill1; // EnemySkill1 인스턴스를 저장할 필드
    public EnemySkill2 bossSkill2; // EnemySkill1 인스턴스를 저장할 필드

    public EnemySkillState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        bossEnemy = stateMachine.Enemy as BossEnemy;
        bossSkill1 = new EnemySkill1(bossEnemy, stateMachine);
        bossSkill2 = new EnemySkill2(bossEnemy, stateMachine);
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
        enemy.slash.SetActive(true);
        bossEnemy.skillChargingEffect.SetActive(false);
        foreach (Transform child in bossEnemy.skillZone.transform)
            child.gameObject.SetActive(false);

        bossSkill1.SkillAttack1();
    }

    public void SkeletonSkillBossStart()
    {
        bossEnemy.skillChargingEffect.SetActive(false);
        foreach (Transform child in bossEnemy.skillZone.transform)
            child.gameObject.SetActive(false);

        bossSkill2.SkillAttack2();
    }
}
