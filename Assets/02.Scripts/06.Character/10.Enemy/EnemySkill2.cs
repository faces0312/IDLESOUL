using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill2 : EnemySkillBase
{
    private float skillZoneTime = 2f;

    public EnemySkill2(BossEnemy bossEnemy, EnemyStateMachine stateMachine) : base(bossEnemy, stateMachine) { }

    public override IEnumerator PerformSkill()
    {
        Debug.Log("스킬2시작");
        Vector3 originalScale = new Vector3(0, 0, 1);

        bossEnemy.skillZone.SetActive(true);
        bossEnemy.skillZone.transform.position = bossEnemy.target.transform.position;

        float elapsedTime = 0f;
        while (elapsedTime < skillZoneTime)
        {
            float scale = Mathf.Lerp(0, 6, elapsedTime / skillZoneTime);
            bossEnemy.skillZone.transform.localScale = new Vector3(scale, scale, originalScale.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        bossEnemy.skillZone.SetActive(false);

        SkillAttack2();

        stateMachine.ChangeState(stateMachine.MoveState);
        Debug.Log("스킬2종료");
    }


    public void SkillAttack2()
    {

    }
}
