using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    public List<EnemySkillBase> skill;//보스가 사용할 스킬들
    public GameObject skillZone;

    public float skillSpeed;
    public float skillSpeedTmp;

    public override void Update()
    {
        base.Update();

        skillSpeedTmp += Time.deltaTime;
        //TODO :: skillSpeed 설정후 그 값으로 변경
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
