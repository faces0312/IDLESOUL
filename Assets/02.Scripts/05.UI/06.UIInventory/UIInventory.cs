using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : UIBase<InventoryModel, InventoryView, InventoryController>
{
    public override void Start()
    {
        base.Start();
        
        gameObject.SetActive(false);
    }
    
}
