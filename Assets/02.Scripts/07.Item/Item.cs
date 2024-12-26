using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : BaseItem
{
    public bool equip;
    public int stack;

    public override void Initialize(ItemDB data)
    {
        base.Initialize(data);

        stack = 1;
    }
}
