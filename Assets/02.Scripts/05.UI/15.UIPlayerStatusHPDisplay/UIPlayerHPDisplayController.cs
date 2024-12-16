using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class UIPlayerHPDisplayController : UIController
{
    private UIPlayerHPDisplayView stageLabelView;
    public override void OnShow()
    {
        UpdateView();   // 초기 View 갱신
        view.ShowUI();
    }

    public override void OnHide()
    {
        view.HideUI();
    }

    public override void UpdateView()
    {
        BigInteger playerHealth = GameManager.Instance.player.StatHandler.CurrentStat.health;
        Utils.FormatBigInteger(playerHealth);
        view.UpdateUI();
    }
}
