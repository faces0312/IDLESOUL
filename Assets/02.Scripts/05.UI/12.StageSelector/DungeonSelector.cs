using UnityEngine;
using UnityEngine.UI;

public class DungeonSelector : MonoBehaviour
{
    public StageSelector StageSelector;
    private DungeonSelectorController controller;

    [SerializeField] private Button daily;
    [SerializeField] private Button exp;
    [SerializeField] private Button upgrade;
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

        daily.onClick.AddListener(() =>
        {
            StageSelector.SetStageType(Enums.StageType.Daily);
            UIManager.Instance.ShowUI("stageSelectorController");
        });
        exp.onClick.AddListener(() =>
        {
            StageSelector.SetStageType(Enums.StageType.EXP);
            UIManager.Instance.ShowUI("stageSelectorController");
        });
        upgrade.onClick.AddListener(() =>
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