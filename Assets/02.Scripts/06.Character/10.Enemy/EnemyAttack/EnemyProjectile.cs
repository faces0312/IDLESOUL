﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public enum EnemyGrade
{
    Regular,
    Boss
}

public class EnemyProjectile : BaseProjectile
{
    public EnemyGrade enemyGrade;
    public bool isMelee;
    public float attack;
    [SerializeField] private float value = 0.1f; // 해당 투사체의 계수

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (TargetLayer == ((1 << collision.gameObject.layer) | TargetLayer))
    //    {
    //        Debug.Log($"공격이 {collision.gameObject.name}에 충돌");
    //        DamageCaculate(collision.gameObject, 100 * value);

    //        base.ProjectileCollison(collision);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (TargetLayer == ((1 << other.gameObject.layer) | TargetLayer))
        {
            DamageCaculate(other.gameObject, new BigInteger((int)attack));
            KnockBackCaculate(other.gameObject, 12);

            if(enemyGrade == EnemyGrade.Regular)
            {
                ProjectileCollison(other);
            }
            else
            {
                ProjectileRangeCollison(other);
            }
        }
    }
}