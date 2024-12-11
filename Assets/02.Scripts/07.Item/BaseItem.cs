using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseItem 
{
    private ItemDB itemData;

    public ItemDB ItemData { get => itemData; set => itemData = value; }

    public virtual void Initialize(ItemDB data )
    {
        itemData = data;
    }
}
