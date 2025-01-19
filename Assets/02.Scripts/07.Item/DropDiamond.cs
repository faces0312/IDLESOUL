using System;
using Unity.VisualScripting;
using UnityEngine;
using ScottGarland;

//Enemy�� óġ�ϰ� ������ ���̾Ƹ��(��ȭ) ��ũ��Ʈ �Դϴ�. 
public class DropDiamond : BaseDropItem
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Ÿ���� �÷��̾� ���̾��ΰ�� ������ ���� 
        if (playerLayer == (1 << collision.gameObject.layer | playerLayer))
        {
            GameManager.Instance.player.UserData.Diamonds += enemy.enemyDB.DropDia;

            GameManager.Instance.curDropItemCount--;
            gameObject.SetActive(false);
        }
    }
}
