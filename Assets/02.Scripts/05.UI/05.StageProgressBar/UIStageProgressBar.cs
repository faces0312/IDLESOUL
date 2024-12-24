using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

//Model : GameManager의 처치한 Enemy Count가 Model이됨
//View : UIStageProgressBarView
//Controller : UIStageProgressBarController



public class UIStageProgressBar : UIBase<UIStageProgressBarModel, UIStageProgressBarView, UIStageProgressBarController>
{
    //public string UIKey;

    //private UIStageProgressBarModel model;
    //[SerializeField]private UIStageProgressBarView view;
    //private UIStageProgressBarController controller;

    public override void Start()
    {
        //Model(Data) 초기화
        model = StageManager.Instance.StageProgressModel;
        base.Start();
        //controller = new UIStageProgressBarController();
        //controller.Initialize(view, model);

        //UIManager.Instance.RegisterController(UIKey, controller);
    }

}
