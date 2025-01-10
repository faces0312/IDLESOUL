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
            UIManager.Instance.HideUI<UIStageProgressBarController>();
            GameManager.Instance.GoldDungeon();
            UIManager.Instance.tryBoss.SetActive(false);
            HideUI();
        });
        past.onClick.AddListener(() =>
        {
            StageSelector.SetStageType(Enums.StageType.EXP);
            UIManager.Instance.ShowUI("stageSelectorController");
            HideUI();
        });
        develop.onClick.AddListener(() =>
        {
            
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