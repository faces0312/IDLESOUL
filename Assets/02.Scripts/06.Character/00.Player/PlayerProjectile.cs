using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class PlayerProjectile : BaseProjectile
{
    private int value = 1; // 해당 투사체의 계수(%)
    private BigInteger atkHealAmount;
    private int HealAmount = 10; // 평타 공격시 회복 계수 , 10%
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


    private void OnTriggerEnter(Collider other)
    {
        if (TargetLayer == ((1 << other.gameObject.layer) | TargetLayer))
        {

            //ToDoCode : 데미지 오차 범위 만들것

            BigInteger Damage = BigInteger.Multiply(GameManager.Instance.player.StatHandler.CurrentStat.atk, value);
            atkHealAmount = BigInteger.Divide(Damage, HealAmount); // 적용된 데미지의 HealAmount 만큼 피흡 

            DamageCaculate(other.gameObject, Damage);
            //적에게 공격을 맞출때마다 피회복하는 기능 
            GameManager.Instance.player.BaseHpSystem.TakeHeal(atkHealAmount, GameManager.Instance.player.StatHandler);

            KnockBackCaculate(other.gameObject, 0.0f);
            base.ProjectileCollison(other);


            //오브젝트 풀에서 하나 꺼내서 사용했으니 다시 풀에다 반환하는 메서드코드
            ObjectPoolManager.Instance.GetPool(Const.PLAYER_PROJECTILE_ENERGYBOLT_KEY, Const.POOL_KEY_PLAYERPROJECTILE).GetObject();
        }
    }

}