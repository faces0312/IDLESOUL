using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonDDOL<UIManager>
{
    public RectTransform uiLobbyCanvas;
    public RectTransform popupCanvas;
    public GameObject tryBoss;
    private Dictionary<string, UIController> controllers = new Dictionary<string, UIController>();
    private UIController activeController;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Init()
    {
        uiLobbyCanvas = Instantiate(Resources.Load<RectTransform>("Prefabs/UI/UILobbyCanvas"));
        popupCanvas = Instantiate(Resources.Load<RectTransform>("Prefabs/UI/PopupCanvas_copy"));
        Instantiate(Resources.Load<RectTransform>("Prefabs/UI/UIInventory"));
        Instantiate(Resources.Load<RectTransform>("Prefabs/UI/UIBossSummonAlarm"));

        var SoulStatus = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Soul"), uiLobbyCanvas);
        var SoulBtns = Instantiate(Resources.Load<GameObject>("Prefabs/UI/SoulButtons"), uiLobbyCanvas);
        //var Cursor = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Cursor"), uiLobbyCanvas);
        Instantiate(Resources.Load<GameObject>("Prefabs/UI/PlayerInfoPanel"), uiLobbyCanvas);
        Instantiate(Resources.Load<GameObject>("Prefabs/UI/GachaEffect"));
        Instantiate(Resources.Load<GameObject>("Prefabs/UI/ConversationUI"));

        SoulStatus.transform.SetAsLastSibling();
        SoulBtns.transform.SetAsFirstSibling();
        //Cursor.transform.SetAsFirstSibling();
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
        if (controllers.TryGetValue(key, out UIController controller))
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

    //Key값을 컨트롤러 클래스명으로 교체 후 , 제네릭을 사용 (문자열 누락 방지를 하기위해)
    //20241223 추가

    public void ShowUI<T>() where T : UIController
    {
        if (controllers.ContainsKey(typeof(T).ToString()))
        {
            activeController = controllers[typeof(T).ToString()] as T;
            activeController.OnShow();
        }
    }
    public void HideUI<T>() where T : UIController
    {
        if (controllers.ContainsKey(typeof(T).ToString()))
        {
            activeController = controllers[typeof(T).ToString()] as T;
            activeController.OnHide();
        }
    }
    public void UpdateUI<T>() where T : UIController
    {
        if (controllers.ContainsKey(typeof(T).ToString()))
        {
            controllers[typeof(T).ToString()].UpdateView();
        }
    }

    public T GetController<T>() where T : UIController
    {
        if (controllers.ContainsKey(typeof(T).ToString()))
        {
            return controllers[typeof(T).ToString()] as T;
        }

        return null;
    }

    //모든 PopUpUI를 비활성화하는 메서드
    public void AllHidePopUpUI()
    {
        //PopUpUI 목록//
        /*
         "Prefabs/UI/PopupCanvas_copy"
         "Prefabs/UI/UIInventory"
         "Prefabs/UI/Soul"
         "Prefabs/UI/PlayerInfoPanel"
         "Prefabs/UI/ConversationUI"
         */

        HideUI<InventoryController>();
        HideUI<ItemStatusController>();
        HideUI<PlayerInfoController>();

        //ToDoCode : ShopController 옜날 구조로 작성되어있음 수정 해야됨 
        HideUI("shopController");
        HideUI("dungeonSelectorController");
    }
}
