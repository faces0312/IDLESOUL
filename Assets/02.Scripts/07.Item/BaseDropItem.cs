using System;
using Unity.VisualScripting;
using UnityEngine;

//Enemy를 처치하고 나오는 아이템 오브젝트 스크립트입니다.
public abstract class BaseDropItem : MonoBehaviour
{
    [SerializeField] protected DropItem dropItemData;       //드랍 아이템의 Stat데이터
    [SerializeField] protected LayerMask playerLayer;    //

    [SerializeField] protected BoxCollider dropColider;    //Player와 상호작용하는 콜라이더
    [SerializeField] protected Vector3 colliderSize;    //드랍 아이템 판정 범위

    [SerializeField] protected Rigidbody rigid;    //DropItem의 Rigid

    protected Enemy enemy; // 드랍되는 Enemy 
    public DropItem DropItemData { get => dropItemData; }
    public Enemy Enemy { get => enemy; set => enemy = value; }

    public virtual void Awake()
    {
        dropItemData.Initialize(DataManager.Instance.ItemDB.GetByKey(dropItemData.ID));

        if(dropColider == null)
        {
            dropColider = GetComponent<BoxCollider>();
        }
        dropColider.size = colliderSize;

        if(rigid == null)
        {
            rigid = GetComponent<Rigidbody>();
        }
    }

    public virtual void OnEnable()
    {
        rigid.WakeUp();
    }

}