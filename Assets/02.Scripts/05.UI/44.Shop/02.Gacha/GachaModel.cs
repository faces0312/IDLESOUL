public interface IGachableDB
{
    public int GetKey();
    public int GetRairity();
}

public class GachaModel : UIModel
{
    public readonly string SoulPickUpPath = "Sprite/SoulSprite/PickUp/Carmilla";
    public readonly string SoulPath = "Sprite/SoulSprite/PickUp/Normal";
    public readonly string ItemPickUpPath = "Sprite/ItemSprite/PickUp/Weapon";
    public readonly string ItemPath = "Sprite/ItemSprite/PickUp/Normal";
}

public class GachaController : UIController
{
    private GachaView gachaView;
    private GachaModel gachaModel;

    public GachaView GachaView { get => gachaView; set => gachaView = value; }
    public GachaModel GachaModel { get => gachaModel; set => gachaModel = value; }

    public override void Initialize(IUIBase view, UIModel model)
    {
        gachaModel = model as GachaModel;
        gachaView = view as GachaView;

        base.Initialize(gachaView, gachaModel);
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