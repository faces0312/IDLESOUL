using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public Enemy enemy;

    public void RangedAttack()
    {
        enemy.stateMachine.AttackState.RangedAttack();
    }

    public void MeleeAttackStart()
    {
        enemy.stateMachine.AttackState.MeleeAttack();
    }

    public void MeleeAttackBoss()
    {
        enemy.stateMachine.AttackState.MeleeAttackBoss();
    }
    public void MeleeSkillBossCharging()
    {
        enemy.stateMachine.SkillState.bossEnemy.StartSkillCoroutine(enemy.stateMachine.SkillState.PerformSkill());
    }

    public void MeleeSkillBossStart()
    {
        enemy.stateMachine.SkillState.MeleeSkillBossStart();
    }

    public void MeleeSkillBossEnd()
    {
        enemy.stateMachine.AttackState.meleeAttack.SetActive(false);
        enemy.stateMachine.ChangeState(enemy.stateMachine.MoveState);
    }

    public void EnemyBossDie()
    {
        if (enemy is BossEnemy bossEnemy)
        {
            // skillZone 비활성화
            foreach (Transform child in bossEnemy.skillZone.transform)
                child.gameObject.SetActive(false);
            bossEnemy.skillChargingEffect.SetActive(false);
        }
        enemy.Die();
        GameManager.Instance.GameClear();
    }

    public void EnemyDie()
    {
        enemy.Die();
    }

    public void MeleeAttackEnd()
    {
        enemy.stateMachine.AttackState.meleeAttack.SetActive(false);
    }
}
