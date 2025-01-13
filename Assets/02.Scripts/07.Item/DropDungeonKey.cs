using System;
using Unity.VisualScripting;
using UnityEngine;
using ScottGarland;

//Enemy�� óġ�ϰ� ������ ���������(��ȭ) ��ũ��Ʈ �Դϴ�. 
public class DropDungeonKey : BaseDropItem
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
            //ToDoCode : ���� ����� Ƽ��(����)   
            GameManager.Instance.player.UserData.DungeonKey++;
            UIManager.Instance.ShowUI<UICurGainKeyCountController>();

            GameManager.Instance.curDropItemCount--;
            gameObject.SetActive(false);
        }
    }
}
