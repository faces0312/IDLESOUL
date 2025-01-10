using System;
using Unity.VisualScripting;
using UnityEngine;

//Enemy�� óġ�ϰ� ������ ������ ������Ʈ ��ũ��Ʈ�Դϴ�.
public abstract class BaseDropItem : MonoBehaviour
{
    [SerializeField] protected DropItem dropItemData;       //��� �������� Stat������
    [SerializeField] protected LayerMask playerLayer;    //

    protected Enemy enemy; // ����Ǵ� Enemy 
    public DropItem DropItemData { get => dropItemData; }
    public Enemy Enemy { get => enemy; set => enemy = value; }

    public virtual void Awake()
    {
        dropItemData.Initialize(DataManager.Instance.ItemDB.GetByKey(dropItemData.ID));
    }


}