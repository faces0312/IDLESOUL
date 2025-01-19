using System;
using Unity.VisualScripting;
using UnityEngine;

//Enemy�� óġ�ϰ� ������ ������ ������Ʈ ��ũ��Ʈ�Դϴ�.
public abstract class BaseDropItem : MonoBehaviour
{
    [SerializeField] protected DropItem dropItemData;       //��� �������� Stat������
    [SerializeField] protected LayerMask playerLayer;    //

    [SerializeField] protected BoxCollider dropColider;    //Player�� ��ȣ�ۿ��ϴ� �ݶ��̴�
    [SerializeField] protected Vector3 colliderSize;    //��� ������ ���� ����

    [SerializeField] protected Rigidbody rigid;    //DropItem�� Rigid

    protected Enemy enemy; // ����Ǵ� Enemy 
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