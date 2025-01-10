using System;
using Unity.VisualScripting;
using UnityEngine;
using ScottGarland;

//Enemy�� óġ�ϰ� ������ ���(��ȭ) ��ũ��Ʈ �Դϴ�. 
public class DropGoldCoin : BaseDropItem
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
            GameManager.Instance.player.UserData.Gold += enemy.enemyDB.DropGold;

            gameObject.SetActive(false);
        }
    }
}
