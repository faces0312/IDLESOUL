using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : BossEnemy
{
    void Start()
    {
        skill = new List<EnemySkillBase>
        {
            new EnemySkill2(this, stateMachine)
        };
    }
}
