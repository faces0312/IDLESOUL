using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : UIBase<InventoryModel, InventoryView, InventoryController>
{
    public override void Start()
    {
        //Model(Data) 초기화
        model = new InventoryModel();
        base.Start();
        
        gameObject.SetActive(false);
    }
    
}
