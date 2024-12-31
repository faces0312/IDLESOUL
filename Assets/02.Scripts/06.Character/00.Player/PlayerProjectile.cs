using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class PlayerProjectile : BaseProjectile
{
    private int value = 1; // �ش� ����ü�� ���(%)
    private BigInteger atkHealAmount;
    private int HealAmount = 10; // ��Ÿ ���ݽ� ȸ�� ��� , 10%
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

            //ToDoCode : ������ ���� ���� �����

            BigInteger Damage = BigInteger.Multiply(GameManager.Instance.player.StatHandler.CurrentStat.atk, value);
            atkHealAmount = BigInteger.Divide(Damage, HealAmount); // ����� �������� HealAmount ��ŭ ���� 

            DamageCaculate(other.gameObject, Damage);
            //������ ������ ���⶧���� ��ȸ���ϴ� ��� 
            GameManager.Instance.player.BaseHpSystem.TakeHeal(atkHealAmount, GameManager.Instance.player.StatHandler);

            KnockBackCaculate(other.gameObject, 0.0f);
            base.ProjectileCollison(other);


            //������Ʈ Ǯ���� �ϳ� ������ ��������� �ٽ� Ǯ���� ��ȯ�ϴ� �޼����ڵ�
            ObjectPoolManager.Instance.GetPool(Const.PLAYER_PROJECTILE_ENERGYBOLT_KEY, Const.POOL_KEY_PLAYERPROJECTILE).GetObject();
        }
    }

}