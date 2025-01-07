using System;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Item : BaseItem
{
    [SerializeField] private int ID; // Item ID, ����ȭ �ϱ� ���� 
    public bool IsGain; //�÷��̾��� ������ ù ��������
    public bool equip; //�÷��̾��� ������ ���� ����
    public int stack; //������ ���� ���� 

    public Stat PassiveStat; //������ ���� ȿ��
    public int PassiveStatValue = 5; //������ ������ n%�� �нú� ȿ���� ����� 

    public int UpgradeLevel; // ������ ���� 
    public int UpgradeLevelMax; //������ ��ȭ �ִ� ����
    public int UpgradeStackCount; //������ ��ȭ ���� �ʿ�� 
    public int UpgradeStatIncreaseRatio; //������ ��ȭ�� ���� ������
    public int UpgradeCostIncreaseRatio; //������ ��ȭ ���� ������

    public override void Initialize(ItemDB data)
    {
        base.Initialize(data);

        PassiveStat = ItemStat / PassiveStatValue; // �нú�� ����ȿ���� n�ۼ�Ʈ ȿ���� ����

        ID = itemStat.iD;
        //ToDo : �÷��̾� ���������͸� �����Ͽ� �����Ҽ� �ְ� �ؾߵ�
        IsGain = false;
        stack = 1;
        UpgradeLevel = 1;
        UpgradeLevelMax = 10;
        UpgradeStackCount = 1;
        UpgradeStatIncreaseRatio = 2;
        UpgradeCostIncreaseRatio = 3;
    }

    public void LoadInitData(UserItemData itemData)
    {
        stack = itemData.GainStack;
        UpgradeLevel = itemData.Level;

        ItemStat.maxHealth *= UpgradeStatIncreaseRatio * UpgradeLevel;
        ItemStat.atk *= UpgradeStatIncreaseRatio * UpgradeLevel;
        ItemStat.def *= UpgradeStatIncreaseRatio * UpgradeLevel;
        ItemStat.reduceDamage *= UpgradeStatIncreaseRatio * UpgradeLevel;
        ItemStat.critChance *= UpgradeStatIncreaseRatio * UpgradeLevel;
        ItemStat.critDamage *= UpgradeStatIncreaseRatio * UpgradeLevel;

        UpgradeStackCount *= UpgradeCostIncreaseRatio * UpgradeLevel;

        PassiveStat = ItemStat / PassiveStatValue;

        equip = itemData.isEquip;
    }
}
