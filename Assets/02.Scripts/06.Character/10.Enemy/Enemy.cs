using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int ID;
    public string Name;
    public string Description;// Ȯ�强�� ���ؼ� �߰�����, ���X
    public int DropGoldMin; // ���� ��ȭ ��� �ּ�
    public int DropGoldMax; // ���� ��ȭ ��� �ִ�
    public int DropDiamondMin;  // ��í ��ȭ �ּ�
    public int DropDiamondMax;  // ��í ��ȭ �ִ�

    public StatHandler statHandler; //���� ���� Ŭ����

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
