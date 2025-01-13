using System;
using Unity.VisualScripting;
using UnityEngine;
using ScottGarland;

//Enemy를 처치하고 나오는 던전입장용(재화) 스크립트 입니다. 
public class DropDungeonKey : BaseDropItem
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
            //ToDoCode : 던전 입장용 티켓(열쇠)   
            GameManager.Instance.player.UserData.DungeonKey++;
            UIManager.Instance.ShowUI<UICurGainKeyCountController>();

            GameManager.Instance.curDropItemCount--;
            gameObject.SetActive(false);
        }
    }
}
