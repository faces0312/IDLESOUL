using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : BossEnemy
{
    public float skillSpeed;
    public float skillSpeedTmp;

    public override void Initialize()
    {
        base.Initialize();
        healthBar.gameObject.SetActive(true);
        skill = new List<EnemySkillBase>
        {
            new EnemySkill2(this, stateMachine)
        };
    }

    public override void Update()
    {
        base.Update();

        skillSpeedTmp += Time.deltaTime;
        //TODO :: skillSpeed 설정후 그 값으로 변경
        if (skillSpeedTmp >= 10f)
        {
            Debug.Log("스킬 실행");
            stateMachine.ChangeState(stateMachine.SkillState);
            skillSpeedTmp = 0f;
        }
    }
}
