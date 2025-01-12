using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DropItemData
{
    public int ID; //드랍 아이템의 ID
    public int DropPercent; // 드랍 아이템의 확률
    public int DropCount; //드랍 아이템의 갯수
}

//Enemy가 쓰러질떄 동작하는 컴포넌트임 
//Enemy 마다 컴포넌트로 부착되어있음 
public class EnemyDropItem : MonoBehaviour
{
    public Enemy enemy;
    //[SerializeField] private int[] dropItemsID; // Enemy가 떨어트리는 드랍아이템 ID 목록

    [SerializeField] private DropItemData[] dropItems;  // Enemy가 떨어트리는 드랍아이템 ID 목록

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        enemy.OnDieEvent += DropItem;
    }

    private void DropItem()
    {
        //골드, 다이아 드롭
        //GameManager.Instance.player.UserData.Gold += enemy.enemyDB.DropGold;
        GameManager.Instance.player.UserData.Diamonds += enemy.enemyDB.DropDia;

        //if (dropItemsID.Length > 0 && GameManager.Instance.curDropItemCount < GameManager.Instance.DropItemLimit)
        //{
        //    //Debug - 드랍 아이템 
        //    GameObject DropItem = ObjectPoolManager.Instance.GetPool(Const.DROPITEM_POOL_KEY, dropItemsID[Random.Range(0, dropItemsID.Length)]).GetObject();
        //    DropItem.GetComponent<BaseDropItem>().Enemy = enemy; // 해당 아이템을 떨어뜨린 몬스터를 저장
        //    DropItem.transform.position = enemy.transform.position;

        //    GameManager.Instance.curDropItemCount++; 
        //    DropItem.SetActive(true);
        //}

        if (dropItems.Length > 0 && GameManager.Instance.curDropItemCount < Const.DROPITEM_STAGE_LIMIT_COUNT)
        {
            DropItemPercentCheck(Random.Range(0, 100f));
        }
    }

    private void DropItemPercentCheck(float DropPercent)
    {
        for (int i = 0; i < dropItems.Length ; i++)
        {
            if (DropPercent <= dropItems[i].DropPercent)
            {
                for (int j = 0; j < dropItems[i].DropCount; j++)
                {
                    GameObject DropItem = ObjectPoolManager.Instance.GetPool(Const.DROPITEM_POOL_KEY, dropItems[i].ID).GetObject();
                    DropItem.GetComponent<BaseDropItem>().Enemy = enemy; // 해당 아이템을 떨어뜨린 몬스터를 저장

                    DropItem.transform.position = DropItemPositonSet();

                    GameManager.Instance.curDropItemCount++;
                    DropItem.SetActive(true);
                }
            }
        }

    }

    private Vector3 DropItemPositonSet()
    {
        Vector3 pos = enemy.transform.position;
        pos.x += Random.Range(-1, 2); // x : -1 ~ 1 범위 offset 
        pos.z += Random.Range(-1, 2); // z : -1 ~ 1 범위 offset 
        return pos;
    }
}
