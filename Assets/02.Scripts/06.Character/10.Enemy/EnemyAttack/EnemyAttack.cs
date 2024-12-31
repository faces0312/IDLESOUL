using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class EnemyAttack : MonoBehaviour
{
    public LayerMask TargetLayer; //�ش� ����ü�� ���߱� ���� Ÿ�� ���̾�
    private Enemy enemy; // Enemy ����

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>(); // Enemy ������Ʈ�� ã��
    }
    private void OnTriggerEnter(Collider other)
    {
        if (TargetLayer == ((1 << other.gameObject.layer) | TargetLayer))
        {
            //BigInteger.ToInt32(enemy.StatHandler.CurrentStat.atk)
            //Debug.Log($"������ {other.gameObject.name}�� �浹");
            BigInteger attackPower = new BigInteger((int)enemy.GetAttackPower());
            DamageCaculate(other.gameObject, attackPower);
            KnockBackCaculate(other.gameObject, 0);
        }
    }
    private void DamageCaculate(GameObject hitObject, BigInteger Damage)
    {
        ITakeDamageAble damageable = hitObject.GetComponent<ITakeDamageAble>();
        //TODO :: �����ð��� �ƴҶ����� ���ǿ� �߰��ؾߵ�
        if (damageable != null)
        {
            damageable.TakeDamage(Damage);//�����ѹ� (�÷��̾ Enemy�� Stat���� �޾ƿͼ� ���� ���Ѿߵ�)

        }
    }

    private void KnockBackCaculate(GameObject hitObject, float Power)
    {
        ITakeDamageAble damageable = hitObject.GetComponent<ITakeDamageAble>();
        //TODO :: �����ð��� �ƴҶ����� ���ǿ� �߰��ؾߵ�
        if (damageable != null)
        {
            Vector3 directionKnockBack = (hitObject.transform.position - transform.position).normalized;
            //damageable.TakeKnockBack(directionKnockBack, knockbackPower);
            directionKnockBack.y = 0; // y�� ����
            damageable.TakeKnockBack(directionKnockBack, Power);
        }
    }
}
