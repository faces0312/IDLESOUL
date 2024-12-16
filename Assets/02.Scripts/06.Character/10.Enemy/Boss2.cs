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
        //TODO :: skillSpeed ������ �� ������ ����
        if (skillSpeedTmp >= 10f)
        {
            Debug.Log("��ų ����");
            stateMachine.ChangeState(stateMachine.SkillState);
            skillSpeedTmp = 0f;
        }
    }
}
