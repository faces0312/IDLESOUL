using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulInfo : MonoBehaviour
{
    [SerializeField] private SoulInfoView soulInfoView;
    private SoulInfoController soulInfoController;
    private SoulInfoModel soulInfoModel;

    private string uiKey;

    private void Start()
    {
        uiKey = "SoulInfo";

        if (soulInfoController == null)
        {
            soulInfoModel = new SoulInfoModel();
            soulInfoController = new SoulInfoController();
            soulInfoController.Initialize(soulInfoView, soulInfoModel);
            UIManager.Instance.RegisterController(uiKey, soulInfoController);
        }
        gameObject.SetActive(false);
    }

    public void SoulLevelUp(int amount)
    {
        soulInfoModel.SoulLevelUp(amount);
    }

    public void DefaultLevelUp(int amount)
    {
        soulInfoModel.DefaultLevelUp(amount);
    }

    public void UltimateLevelUp(int amount)
    {
        soulInfoModel.UltimateLevelUp(amount);
    }

    public void PassiveLevelUp(int amount)
    {
        soulInfoModel.PassiveLevelUp(amount);
    }
}
