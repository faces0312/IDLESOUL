using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShopModel : UIModel
{
    public UserData testUser;
    public Dictionary<string, List<ItemDB>> ItemGachaList;
    public Dictionary<string, List<SoulDB>> SoulGachaList;
    public int SoulGachaCount; //�Ŀ� ������ �Ŵ����� ���� �ʿ�
    public int ItemGachaCount;
    public int GachaPrice;
    private string general;


    private void Awake()
    {
        ItemGachaList = new Dictionary<string, List<ItemDB>>();
        SoulGachaList = new Dictionary<string, List<SoulDB>>();
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void DiamondGacha<T>(string Pickup) where T : IGachable
    {
       Type t = typeof(T);

        if (t == typeof(SoulDB))
        {

        }
    }
}