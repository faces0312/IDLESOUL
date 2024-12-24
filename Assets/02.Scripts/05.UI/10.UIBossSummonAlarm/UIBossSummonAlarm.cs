using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBossSummonAlarm : UIBase<UIBossSummonAlarmModel, UIBossSummonAlarmView, UIBossSummonAlarmController>
{
    public override void Start()
    {
        model = new UIBossSummonAlarmModel();
        base.Start();
        controller.OnHide();
    }
}

