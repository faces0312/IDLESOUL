using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour, ITakeDamageAble
{
    protected StatHandler statHandler; //���� ���� Ŭ����
    //protected Animator characterAnimator; //�ִϸ��̼� ���� ��Ʈ�ѷ�
    //Controller CharacterController; //ĳ���� ��Ʈ�ѷ�

    public abstract void Attack();
    public abstract void Move();

    public virtual void TakeDamage(float damage)
    {
    }

    public virtual void TakeKnockBack(Vector3 direction, float force)
    {
    }
}
