using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    public List<EnemySkillBase> skill;//������ ����� ��ų��
    public GameObject skillZone;

    public float skillSpeed;
    public float skillSpeedTmp;

    public override void Update()
    {
        base.Update();

        skillSpeedTmp += Time.deltaTime;
        //TODO :: skillSpeed ������ �� ������ ����
        if (skillSpeedTmp >= 5f)
        {
            stateMachine.ChangeState(stateMachine.SkillState);
            skillSpeedTmp = 0f;
        }
    }

    public void StartSkillCoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }
}
