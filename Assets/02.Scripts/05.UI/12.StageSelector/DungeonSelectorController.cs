using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class DungeonSelectorController : UIController
{
    public string key = "dungeonSelectorController";
    public GameObject DungeonSelector;
    public DungeonSelectorModel dModel;
    public DungeonSelectorView dView;

    public override void Initialize(IUIBase view, UIModel model)
    {
        dModel = model as DungeonSelectorModel;
        dView = view as DungeonSelectorView;

        base.Initialize(dView, dModel);
    }

    public override void OnHide()
    {
        dView.HideUI();
    }

    public override void OnShow()
    {
        dView.ShowUI();
    }

    public override void UpdateView()
    {
        
    }

    public void OnClick()
    {
        
    }
}