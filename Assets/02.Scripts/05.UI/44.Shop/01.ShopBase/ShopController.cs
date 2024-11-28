public class ShopController : UIController
{
    private ShopModel shopModel;

    public override void Initialize(IUIBase view, UIModel model)
    {
        base.Initialize(view, model);

        shopModel = model as ShopModel;

    }

    public override void OnHide()
    {
        throw new System.NotImplementedException();
    }

    public override void OnShow()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateView()
    {
        throw new System.NotImplementedException();
    }
}