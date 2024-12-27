using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossEnemy : Enemy
{
    public List<EnemySkillBase> skill;//보스가 사용할 스킬들
    public GameObject skillZone;
    public GameObject skillChargingEffect;
    public static float skillDamage;

    public void StartSkillCoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

    public override void BossAppear()
    {
        IsInvulnerable = true;
        StartCoroutine(BossAppearCoroutine());
    }

    IEnumerator BossAppearCoroutine()
    {
        yield return new WaitForSeconds(2.5f);
        IsInvulnerable = false;
    }

}