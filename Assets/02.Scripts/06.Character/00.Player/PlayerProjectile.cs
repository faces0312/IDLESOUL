using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class PlayerProjectile : BaseProjectile
{
    private float value = 1.0f; // 해당 투사체의 계수

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
    //        int Atk = BigInteger.ToInt32(GameManager.Instance._player.UserData.stat.atk);

    //        Debug.Log($"공격이 {collision.gameObject.name}에 충돌");
    //        DamageCaculate(collision.gameObject , Atk * value);

    //        base.ProjectileCollison(collision);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (TargetLayer == ((1 << other.gameObject.layer) | TargetLayer))
        {
            BigInteger Atk = GameManager.Instance.player.StatHandler.CurrentStat.atk;
            //데미지 오차 범위 만들것

            DamageCaculate(other.gameObject, BigInteger.Multiply(Atk, (int)value));//계수 수정 필요함 
            KnockBackCaculate(other.gameObject, 0.0f);
            base.ProjectileCollison(other);

            ObjectPoolManager.Instance.GetPool(Const.PLAYER_PROJECTILE_ENERGYBOLT_KEY, Const.POOL_KEY_PLAYERPROJECTILE).GetObject();
        }
    }

}