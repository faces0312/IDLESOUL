using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DropItemData
{
    public int ID; //��� �������� ID
    public int DropPercent; // ��� �������� Ȯ��
    public int DropCount; //��� �������� ����
}

//Enemy�� �������� �����ϴ� ������Ʈ�� 
//Enemy ���� ������Ʈ�� �����Ǿ����� 
public class EnemyDropItem : MonoBehaviour
{
    public Enemy enemy;
    //[SerializeField] private int[] dropItemsID; // Enemy�� ����Ʈ���� ��������� ID ���

    [SerializeField] private DropItemData[] dropItems;  // Enemy�� ����Ʈ���� ��������� ID ���

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        enemy.OnDieEvent += DropItem;
    }

    private void DropItem()
    {
        //���, ���̾� ���
        //GameManager.Instance.player.UserData.Gold += enemy.enemyDB.DropGold;
        GameManager.Instance.player.UserData.Diamonds += enemy.enemyDB.DropDia;

        //if (dropItemsID.Length > 0 && GameManager.Instance.curDropItemCount < GameManager.Instance.DropItemLimit)
        //{
        //    //Debug - ��� ������ 
        //    GameObject DropItem = ObjectPoolManager.Instance.GetPool(Const.DROPITEM_POOL_KEY, dropItemsID[Random.Range(0, dropItemsID.Length)]).GetObject();
        //    DropItem.GetComponent<BaseDropItem>().Enemy = enemy; // �ش� �������� ����߸� ���͸� ����
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
                    DropItem.GetComponent<BaseDropItem>().Enemy = enemy; // �ش� �������� ����߸� ���͸� ����

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
        pos.x += Random.Range(-1, 2); // x : -1 ~ 1 ���� offset 
        pos.z += Random.Range(-1, 2); // z : -1 ~ 1 ���� offset 
        return pos;
    }
}
