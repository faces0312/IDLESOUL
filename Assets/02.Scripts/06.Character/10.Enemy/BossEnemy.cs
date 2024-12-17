using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossEnemy : Enemy
{
    public List<EnemySkillBase> skill;//������ ����� ��ų��
    public GameObject skillZone;
    public GameObject skillChargingEffect;

    public void StartSkillCoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

}