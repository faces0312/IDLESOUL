using System;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Item : BaseItem
{
    [SerializeField] private int ID; // Item ID, 직렬화 하기 위한 
    public bool IsGain; //플레이어의 아이템 첫 소지여부
    public bool equip; //플레이어의 아이템 장착 여부
    public int stack; //아이템 소지 갯수 

    public Stat PassiveStat; //아이템 소지 효과
    public int PassiveStatValue = 5; //아이템 스텟의 n%가 패시브 효과로 적용됨 

    public int UpgradeLevel; // 아이템 레벨 
    public int UpgradeLevelMax; //아이템 강화 최대 레벨
    public int UpgradeStackCount; //아이템 강화 스택 필요양 
    public int UpgradeStatIncreaseRatio; //아이템 강화시 스텟 증가량
    public int UpgradeCostIncreaseRatio; //아이템 강화 스택 증가량

    public override void Initialize(ItemDB data)
    {
        base.Initialize(data);

        PassiveStat = ItemStat / PassiveStatValue; // 패시브는 고유효과의 n퍼센트 효과를 지님

        ID = itemStat.iD;
        //ToDo : 플레이어 유저데이터를 참고하여 갱신할수 있게 해야됨
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
