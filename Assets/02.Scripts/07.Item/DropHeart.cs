using System;
using Unity.VisualScripting;
using UnityEngine;
using ScottGarland;

//Enemy를 처치하고 나오는 회복 아이템 스크립트 입니다. 
public class DropHeart : BaseDropItem
{

    public override void Awake()
    {
        base.Awake();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //타겟이 플레이어 레이어인경우 아이템 적용 
        if (playerLayer == (1 << collision.gameObject.layer | playerLayer))
        {
            //회복 아이템이 퍼센트 적용인경우 
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
