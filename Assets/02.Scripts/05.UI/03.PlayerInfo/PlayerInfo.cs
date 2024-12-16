using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private PlayerInfoView infoView;
    private PlayerInfoController infoController;
    private PlayerInfoModel infoModel;

    private string uiKey;

    private void Awake()
    {
        uiKey = "PlayerInfo";

        if (infoController == null)
        {
            infoModel = new PlayerInfoModel();
            infoController = new PlayerInfoController();
            infoController.Initialize(infoView, infoModel);
            UIManager.Instance.RegisterController(uiKey, infoController);
        }
        gameObject.SetActive(false);
    }

}
