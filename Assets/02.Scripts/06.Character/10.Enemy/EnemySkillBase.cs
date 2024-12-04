using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySkillBase
{
    protected BossEnemy bossEnemy;
    protected EnemyStateMachine stateMachine;

    //클래스의 필드 초기화
    public EnemySkillBase(BossEnemy bossEnemy, EnemyStateMachine stateMachine)
    {
        this.bossEnemy = bossEnemy;
        this.stateMachine = stateMachine;
    }

    public abstract IEnumerator PerformSkill();
}
