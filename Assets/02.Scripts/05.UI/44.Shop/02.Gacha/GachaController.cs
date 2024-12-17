using System;
using UnityEngine.InputSystem;
using UnityEngine;

public class GachaController : UIController
{
    public string key = "gachaController";

    public GameObject GachaPanel;

    public override void OnHide()
    {
        GachaPanel.SetActive(false);
    }

    public override void OnShow()
    {
        GachaPanel.SetActive(true);
    }

    public override void UpdateView()
    {
        
    }
}