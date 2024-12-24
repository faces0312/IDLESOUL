using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInfo : UIBase<PlayerInfoModel,PlayerInfoView, PlayerInfoController>
{
    public override void Start()
    {
        model = new PlayerInfoModel();
        base.Start();

        gameObject.SetActive(false);
    }

    public void HpLevelUp(int amount)
    {
        model.HpLevelUp(amount);
    }

    public void AtkLevelUp(int amount)
    {
        model.AtkLevelUp(amount);
    }

    public void DefLevelUp(int amount)
    {
        model.DefLevelUp(amount);
    }

    public void ReduceDmgLevelUp(int amount)
    {
        model.ReduceDmgLevelUp(amount);
    }

    public void CritChanceLevelUp(int amount)
    {
        model.CritChanceLevelUp(amount);
    }

    public void CritDmgLevelUp(int amount)
    {
        model.CritDmgLevelUp(amount);
    }
}
