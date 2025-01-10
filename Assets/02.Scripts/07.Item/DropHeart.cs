using System;
using Unity.VisualScripting;
using UnityEngine;
using ScottGarland;

//Enemy�� óġ�ϰ� ������ ȸ�� ������ ��ũ��Ʈ �Դϴ�. 
public class DropHeart : BaseDropItem
{

    public override void Awake()
    {
        base.Awake();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Ÿ���� �÷��̾� ���̾��ΰ�� ������ ���� 
        if (playerLayer == (1 << collision.gameObject.layer | playerLayer))
        {
            //ȸ�� �������� �ۼ�Ʈ �����ΰ�� 
            if(dropItemData.ItemData.HealthPercent)
            {
                GameManager.Instance.player.TakeHeal(
                    BigInteger.Divide(
                     GameManager.Instance.player.StatHandler.CurrentStat.maxHealth *
                     dropItemData.ItemStat.maxHealth,
                     100
                     )
                     );
            }
            else
            {
                GameManager.Instance.player.TakeHeal(dropItemData.ItemStat.maxHealth);
            }

          
            gameObject.SetActive(false);
        }
    }
}
