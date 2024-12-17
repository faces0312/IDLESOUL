using UnityEngine;
using UnityEngine.InputSystem;

public class DungeonSelectorController : UIController
{
    public string key = "dungeonSelectorController";
    public GameObject DungeonSelector;
    public override void OnHide()
    {
        DungeonSelector.SetActive(false);
    }

    public override void OnShow()
    {
        DungeonSelector.SetActive(true);
    }

    public override void UpdateView()
    {
        
    }
}