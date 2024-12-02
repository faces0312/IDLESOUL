using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySkillBase
{
    protected BossEnemy bossEnemy;
    protected EnemyStateMachine stateMachine;

    public EnemySkillBase(BossEnemy bossEnemy, EnemyStateMachine stateMachine)
    {
        this.bossEnemy = bossEnemy;
        this.stateMachine = stateMachine;
    }

    public abstract IEnumerator PerformSkill();
}
