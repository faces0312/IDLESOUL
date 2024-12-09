using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

//Model : GameManager의 처치한 Enemy Count가 Model이됨
//View : UIStageProgressBarView
//Controller : UIStageProgressBarController

public class UIStageProgressBar : MonoBehaviour
{
    public string UIKey;

    private UIStageProgressBarModel model;
    [SerializeField]private UIStageProgressBarView view;
    private UIStageProgressBarController controller;

    private void Awake()
    {
        model = new UIStageProgressBarModel();
        GameManager.Instance.StageProgressModel = model;
        controller = new UIStageProgressBarController();
        controller.Initialize(view, model);

        UIManager.Instance.RegisterController(UIKey, controller);
    }

}
