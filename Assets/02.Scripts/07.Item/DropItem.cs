using System;
using Unity.VisualScripting;
using UnityEngine;

//Enemy�� óġ�ϰ� ������ ������ ������ ��ũ��Ʈ�Դϴ�. 
[System.Serializable]
public class DropItem : BaseItem
{
    [SerializeField] private int iD; //�ش� �������� ID

    public int ID { get => iD; }

    public override void Initialize(ItemDB data)
    {
        base.Initialize(data);
    }
}
