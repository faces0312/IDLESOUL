using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : MonoBehaviour
{
    public float attack;
    public LayerMask TargetLayer; //�ش� ����ü�� ���߱� ���� Ÿ�� ���̾�

    private void OnTriggerEnter(Collider other)
    {
        if (TargetLayer == ((1 << other.gameObject.layer) | TargetLayer))
        {
            Debug.Log($"������ {other.gameObject.name}�� �浹");
            DamageCaculate(other.gameObject, BossEnemy.skillDamage);
            KnockBackCaculate(other.gameObject, 0);
        }
    }

    private void DamageCaculate(GameObject hitObject, float Damage)
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
