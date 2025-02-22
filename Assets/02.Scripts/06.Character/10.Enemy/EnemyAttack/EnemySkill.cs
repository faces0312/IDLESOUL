using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class EnemySkill : MonoBehaviour
{
    public float attack;
    public LayerMask TargetLayer; //해당 투사체를 맞추기 위한 타겟 레이어

    private void OnEnable()
    {
        StartCoroutine("DisAble");
    }

    IEnumerator DisAble()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TargetLayer == ((1 << other.gameObject.layer) | TargetLayer))
        {
            Debug.Log($"공격이 {other.gameObject.name}에 충돌");
            DamageCaculate(other.gameObject, new BigInteger((int)BossEnemy.skillDamage));
            KnockBackCaculate(other.gameObject, 0);
        }
    }

    private void DamageCaculate(GameObject hitObject, BigInteger Damage)
    {
        ITakeDamageAble damageable = hitObject.GetComponent<ITakeDamageAble>();
        //TODO :: 무적시간이 아닐때에도 조건에 추가해야됨
        if (damageable != null)
        {
            damageable.TakeDamage(Damage);//매직넘버 (플레이어나 Enemy의 Stat값을 받아와서 적용 시켜야됨)

        }
    }

    private void KnockBackCaculate(GameObject hitObject, float Power)
    {
        ITakeDamageAble damageable = hitObject.GetComponent<ITakeDamageAble>();
        //TODO :: 무적시간이 아닐때에도 조건에 추가해야됨
        if (damageable != null)
        {
            Vector3 directionKnockBack = (hitObject.transform.position - transform.position).normalized;
            //damageable.TakeKnockBack(directionKnockBack, knockbackPower);
            directionKnockBack.y = 0; // y축 보정
            damageable.TakeKnockBack(directionKnockBack, Power);
        }
    }
}
