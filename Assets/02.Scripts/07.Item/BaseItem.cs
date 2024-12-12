using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScottGarland;

public abstract class BaseItem 
{
    private ItemDB itemData;
    private Stat itemStat = new Stat();

    public ItemDB ItemData { get => itemData; set => itemData = value; }
    public Stat ItemStat { get => itemStat; }

    public virtual void Initialize(ItemDB data)
    {
        itemData = data;

        itemStat.iD = itemData.key;
        itemStat.maxHealth = new BigInteger((long)itemData.Health);
        itemStat.atk = new BigInteger((long)itemData.Attack);
        itemStat.def = new BigInteger((long)itemData.Defence);
        itemStat.critChance = itemData.CritChance;
        itemStat.critDamage = itemData.CritDamage; 
    }
}
