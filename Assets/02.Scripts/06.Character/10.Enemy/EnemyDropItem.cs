using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy가 쓰러질떄 동작하는 컴포넌트임 
//Enemy 마다 컴포넌트로 부착되어있음 
public class EnemyDropItem : MonoBehaviour
{
    public Enemy enemy;
    [SerializeField] private int[] dropItemsID; // Enemy가 떨어트리는 드랍아이템 ID 목록

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        enemy.OnDieEvent += DropItem;
    }

    private void DropItem()
    {
        //골드, 다이아 드롭
        GameManager.Instance.player.UserData.Gold += enemy.enemyDB.DropGold;
        GameManager.Instance.player.UserData.Diamonds += enemy.enemyDB.DropDia;

        if (dropItemsID.Length > 0)
        {
            //Debug - 드랍 아이템 
            ObjectPoolManager.Instance.GetPool(Const.DROPITEM_POOL_KEY, dropItemsID[Random.Range(0, dropItemsID.Length)]).GetObject();
        }
    }
}
