using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossEnemy : Enemy
{
    public List<EnemySkillBase> skill;//보스가 사용할 스킬들
    public bool isSkillCharging;//스킬 차징중인지
    public GameObject skillZone;
    public GameObject skillChargingEffect;
    public static float skillDamage;
    public Vector3 originalScale;

    public void StartSkillCoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

    public override void BossAppear()
    {
        IsInvulnerable = true;
        StartCoroutine(BossAppearCoroutine());
    }


    public override void SetSkillCharging(bool charging)
    {
        isSkillCharging = charging;
        if (charging)
        {
            originalScale = transform.localScale; // 스킬 사용 시작 시의 scale 저장
        }
    }


    IEnumerator BossAppearCoroutine()
    {
        yield return new WaitForSeconds(2.5f);
        IsInvulnerable = false;
    }

    public override void LateUpdate()
    {
        if (!isSkillCharging)
        {
            base.LateUpdate(); // 기본 방향 전환 로직
        }
        else
        {
            transform.localScale = originalScale; // 스킬 사용 시작 시의 scale로 고정
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        isSkillCharging = false;
    }

}