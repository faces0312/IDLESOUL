using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Dictionary<string, UIController> controllers = new Dictionary<string, UIController>();
    private UIController activeController;

    public void RegisterController(string key, UIController controller)
    {
        if(!controllers.ContainsKey(key))
        {
            controllers.Add(key, controller);
        }
    }

    public void ShowUI(string key)
    {
        if(activeController != null)
        {
            activeController.OnHide();
        }

        if(controllers.TryGetValue(key, out UIController controller))
        {
            activeController = controller;
            activeController.OnShow();
        }
    }

    public void UpdateUI(string key)
    {
        if(controllers.TryGetValue(key, out UIController controller))
        {
            controller.UpdateView();
        }
    }
}
