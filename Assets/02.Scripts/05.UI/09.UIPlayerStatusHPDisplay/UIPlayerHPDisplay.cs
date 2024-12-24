using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIPlayerHPDisplay : UIBase<UIPlayerHPDisplayModel, UIPlayerHPDisplayView, UIPlayerHPDisplayController>
{
    public override void Start()
    {
        model = new UIPlayerHPDisplayModel();
        model.Init();

        base.Start();
    }
}

