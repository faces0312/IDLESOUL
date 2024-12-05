using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    public Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        enemy.OnDieEvent += DropItem;
    }

    private void DropItem()
    {
        //���, ���̾� ���
        Debug.Log(GameManager.Instance.player.UserData.Gold);
        GameManager.Instance.player.UserData.Gold += enemy.enemyDB.DropGold;
        GameManager.Instance.player.UserData.Diamonds += enemy.enemyDB.DropDia;
        Debug.Log(GameManager.Instance.player.UserData.Gold);

    }
}
