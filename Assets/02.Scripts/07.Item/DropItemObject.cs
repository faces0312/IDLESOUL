using System;
using Unity.VisualScripting;
using UnityEngine;

//Enemy를 처치하고 나오는 아이템 오브젝트 스크립트입니다.
public class DropItemObject : MonoBehaviour
{
    [SerializeField] private DropItem dropItemData;       //드랍 아이템의 Stat데이터

    public DropItem DropItemData { get => dropItemData; }

    public void Awake()
    {
        dropItemData.Initialize(DataManager.Instance.ItemDB.GetByKey(dropItemData.ID));
    }

}
