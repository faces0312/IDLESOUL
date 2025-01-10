using ScottGarland;
using UnityEngine;

public abstract class BaseItem 
{
    protected ItemDB itemData;
    protected Stat itemStat = new Stat();
    public ItemDB ItemData { get => itemData; }
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
