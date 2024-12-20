using UnityEngine;

public class StageSelectorController : UIController
{
    public string key = "stageSelectorController";
    public GameObject StageSelector;

    public override void OnHide()
    {
        StageSelector.SetActive(false);
    }

    public override void OnShow()
    {
        StageSelector.SetActive(true);
    }

    public override void UpdateView()
    {
        
    }
}