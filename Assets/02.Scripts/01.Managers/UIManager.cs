using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] RectTransform uiLobbyCanvas;
    private Dictionary<string, UIController> controllers = new Dictionary<string, UIController>();
    private UIController activeController;

    private void Start()
    {
        InitUI();
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
        //if(activeController != null)
        //{
        //    activeController.OnHide();
        //}

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

    private void InitUI()
    {
        var obj = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Soul"), uiLobbyCanvas);
        Instantiate(Resources.Load<GameObject>("Prefabs/UI/SoulButtons"), uiLobbyCanvas);
        obj.transform.SetAsLastSibling();
    }
}
