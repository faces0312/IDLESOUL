using System;
using Unity.VisualScripting;
using UnityEngine;

//Enemy를 처치하고 나오는 아이템 데이터 스크립트입니다. 
[System.Serializable]
public class DropItem : BaseItem
{
    [SerializeField] private int iD; //해당 아이템의 ID

    public int ID { get => iD; }

    public override void Initialize(ItemDB data)
    {
        base.Initialize(data);
    }
}
