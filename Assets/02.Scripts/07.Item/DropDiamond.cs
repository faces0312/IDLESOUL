using System;
using Unity.VisualScripting;
using UnityEngine;
using ScottGarland;

//Enemy를 처치하고 나오는 다이아몬드(재화) 스크립트 입니다. 
public class DropDiamond : BaseDropItem
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
            GameManager.Instance.player.UserData.Diamonds += enemy.enemyDB.DropDia;

            GameManager.Instance.curDropItemCount--;
            gameObject.SetActive(false);
        }
    }
}
