using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIItemStatus : UIBase<ItemStatusModel, ItemStatusView, ItemStatusController>
{
    public override void Start()
    {
        //Model(Data) √ ±‚»≠
        model = new ItemStatusModel();
        base.Start();
        gameObject.SetActive(false);
    }
}
