using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : BossEnemy
{
    public float skillSpeed;
    public float skillSpeedTmp;

    protected override void Start()
    {
        base.Start();
        healthBar.gameObject.SetActive(true);
        skill = new List<EnemySkillBase>
        {
            new EnemySkill1(this, stateMachine)
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
