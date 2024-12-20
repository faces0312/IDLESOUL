using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonDDOL<UIManager>
{
    public RectTransform uiLobbyCanvas;
    private Dictionary<string, UIController> controllers = new Dictionary<string, UIController>();
    private UIController activeController;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Init()
    {
        uiLobbyCanvas = Instantiate(Resources.Load<RectTransform>("Prefabs/UI/UILobbyCanvas"));
        Instantiate(Resources.Load<RectTransform>("Prefabs/UI/PopupCanvas_copy"));
        Instantiate(Resources.Load<RectTransform>("Prefabs/UI/UIInventory"));
        Instantiate(Resources.Load<RectTransform>("Prefabs/UI/UIBossSummonAlarm"));

        var SoulStatus = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Soul"), uiLobbyCanvas);
        var SoulBtns = Instantiate(Resources.Load<GameObject>("Prefabs/UI/SoulButtons"), uiLobbyCanvas);
        Instantiate(Resources.Load<GameObject>("Prefabs/UI/PlayerInfoPanel"), uiLobbyCanvas);

        SoulStatus.transform.SetAsLastSibling();
        SoulBtns.transform.SetAsFirstSibling();
    }
    public void RegisterController(string key, UIController controller)
    {
        if(!controllers.ContainsKey(key))
        {
            controllers.Add(key, controller);
        }
    }

    public void ShowUI(string key)
    {
        if(controllers.TryGetValue(key, out UIController controller))
        {
            activeController = controller;
            activeController.OnShow();
        }
    }

    public void HideUI(string key)
    {
        if (controllers.TryGetValue(key, out UIController controller))
        {
            activeController = controller;
            activeController.OnHide();
        }
    }

    public void UpdateUI(string key)
    {
        if(controllers.TryGetValue(key, out UIController controller))
        {
            controller.UpdateView();
        }
    }

    public UIController GetController(string key)
    {
        if (controllers.TryGetValue(key, out UIController controller))
        {
            return controller;
        }

        return null;
    }
}
