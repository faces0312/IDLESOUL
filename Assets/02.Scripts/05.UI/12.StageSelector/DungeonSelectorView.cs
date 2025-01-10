using UnityEngine;
using UnityEngine.UI;

public class DungeonSelectorView : MonoBehaviour, IUIBase
{
    public StageSelector StageSelector;

    [SerializeField] private Button money;
    [SerializeField] private Button past;
    [SerializeField] private Button develop;
    [SerializeField] private Button exit;

    private void Start()
    {
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
            HideUI();
        });

        HideUI();
    }
    public void Initialize()
    {
        
    }

    public void HideUI()
    {
        this.gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        this.gameObject.SetActive(true);
    }

    public void UpdateUI()
    {
        
    }
}