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

    public void HpLevelUp(int amount)
    {
        infoModel.HpLevelUp(amount);
    }

    public void AtkLevelUp(int amount)
    {
        infoModel.AtkLevelUp(amount);
    }

    public void DefLevelUp(int amount)
    {
        infoModel.DefLevelUp(amount);
    }

    public void ReduceDmgLevelUp(int amount)
    {
        infoModel.ReduceDmgLevelUp(amount);
    }

    public void CritChanceLevelUp(int amount)
    {
        infoModel.CritChanceLevelUp(amount);
    }

    public void CritDmgLevelUp(int amount)
    {
        infoModel.CritDmgLevelUp(amount);
    }
}
