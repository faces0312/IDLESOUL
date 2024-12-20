using UnityEngine;

public class GachaResultController : UIController
{
     public GameObject GachaPanel;
     public string key = "gachaResultController";

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