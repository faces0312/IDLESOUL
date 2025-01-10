using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy�� �������� �����ϴ� ������Ʈ�� 
//Enemy ���� ������Ʈ�� �����Ǿ����� 
public class EnemyDropItem : MonoBehaviour
{
    public Enemy enemy;
    [SerializeField] private int[] dropItemsID; // Enemy�� ����Ʈ���� ��������� ID ���

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

        if (dropItemsID.Length > 0 && GameManager.Instance.curDropItemCount < GameManager.Instance.DropItemLimit)
        {
            //Debug - ��� ������ 
            GameObject DropItem = ObjectPoolManager.Instance.GetPool(Const.DROPITEM_POOL_KEY, dropItemsID[Random.Range(0, dropItemsID.Length)]).GetObject();
            DropItem.GetComponent<BaseDropItem>().Enemy = enemy; // �ش� �������� ����߸� ���͸� ����
            DropItem.transform.position = enemy.transform.position;

            GameManager.Instance.curDropItemCount++; 
            DropItem.SetActive(true);
        }
    }
}
