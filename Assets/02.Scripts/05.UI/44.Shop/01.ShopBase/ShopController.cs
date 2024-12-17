using UnityEngine.UIElements.Experimental;

public class ShopController : UIController
{
    string key = "shopController";
    
    private ShopModel shopModel;
    private ShopView shopView;

    public ShopModel ShopModel { get => shopModel; set => shopModel = value; }
    public ShopView ShopView { get => shopView; set => shopView = value; }

    public override void Initialize(IUIBase view, UIModel model)
    {
        shopModel = model as ShopModel;
        shopView = view as ShopView;

        base.Initialize(shopView, shopModel);
    }

    public override void OnHide()
    {
        view.HideUI();
    }

    public override void OnShow()
    {
        view.ShowUI();
    }

    public override void UpdateView()
    {
        view.UpdateUI();
    }
}
