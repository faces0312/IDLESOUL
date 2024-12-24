using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIStageLabel : UIBase<UIStageLabelModel, UIStageLabelView, UIStageLabelController>
{
    //public string UIKey;

    //private UIStageLabelModel model;
    //[SerializeField] private UIStageLabelView views;
    //private UIStageLabelController controller;

    public override void Start()
    {
        model = new UIStageLabelModel();
        base.Start();
        controller.OnShow();
    }
}
