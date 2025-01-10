using UnityEngine;
using UnityEngine.UI;

public class DungeonSelector : MonoBehaviour
{
    public StageSelector StageSelector;
    private DungeonSelectorController controller;

    [SerializeField] private Button money;
    [SerializeField] private Button past;
    [SerializeField] private Button develop;
    [SerializeField] private Button exit;

    private void Awake()
    {
        StageSelector = FindObjectOfType<StageSelector>();
    }

    private void Start()
    {
        controller = new DungeonSelectorController();
        controller.DungeonSelector = this.gameObject;
        UIManager.Instance.RegisterController(controller.key, controller);

        money.onClick.AddListener(() =>
        {
            StageSelector.SetStageType(Enums.StageType.Daily);
            UIManager.Instance.ShowUI("stageSelectorController");
        });
        past.onClick.AddListener(() =>
        {
            StageSelector.SetStageType(Enums.StageType.EXP);
            UIManager.Instance.ShowUI("stageSelectorController");
        });
        develop.onClick.AddListener(() =>
        {
            StageSelector.SetStageType(Enums.StageType.Upgrade);
            UIManager.Instance.ShowUI("stageSelectorController");
        });
        exit.onClick.AddListener(() =>
        {
            controller.OnHide();
        }); 
        this.gameObject.SetActive(false);
    }
}