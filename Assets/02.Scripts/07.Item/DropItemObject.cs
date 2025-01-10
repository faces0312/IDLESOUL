using System;
using Unity.VisualScripting;
using UnityEngine;

//Enemy�� óġ�ϰ� ������ ������ ������Ʈ ��ũ��Ʈ�Դϴ�.
public class DropItemObject : MonoBehaviour
{
    [SerializeField] private DropItem dropItemData;       //��� �������� Stat������

    public DropItem DropItemData { get => dropItemData; }

    public void Awake()
    {
        dropItemData.Initialize(DataManager.Instance.ItemDB.GetByKey(dropItemData.ID));
    }

}
