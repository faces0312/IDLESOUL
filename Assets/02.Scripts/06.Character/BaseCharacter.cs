using System.Collections;
using System.Collections.Generic;
using ScottGarland;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour, ITakeDamageAble
{
    protected StatHandler statHandler; //���� ���� Ŭ����
    //protected Animator characterAnimator; //�ִϸ��̼� ���� ��Ʈ�ѷ�
    //protected CharacterController characterController; //ĳ���� ��Ʈ�ѷ�

    public abstract void Attack();
    public abstract void Move();

    public virtual void TakeDamage(float damage)
    {
        int maxHelth = BigInteger.ToInt32(statHandler.CurrentStat.maxHealth);
        int curHelth = BigInteger.ToInt32(statHandler.CurrentStat.health);
        statHandler.CurrentStat.health = Mathf.Clamp(curHelth - (int)damage, 0, maxHelth);

        Debug.Log($"{gameObject.name} �ǰݵ�! ������ : {damage} , ü�� ���� : {curHelth}");
    }

    public virtual void TakeKnockBack(Vector3 direction, float force)
    {
    }
}
