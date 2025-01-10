using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DungeonSelector : MonoBehaviour
{
    private DungeonSelectorController controller;
    [SerializeField]private DungeonSelectorView view;

    private void Awake()
    {
        controller = new DungeonSelectorController();
        var model = new DungeonSelectorModel();

        controller.dView = view;
        controller.dModel = model;
        controller.DungeonSelector = this.gameObject;

        controller.Initialize(view, model);
        UIManager.Instance.RegisterController(controller.key, controller);
    }
}