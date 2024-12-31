using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementView : MonoBehaviour, IUIBase
{
    [SerializeField] private GameObject AchievePanel;
    [SerializeField] private RecycleScrollY scroll;
    [SerializeField] private Button exit;

    private bool isReceived;

    private void OnEnable()
    {
        scroll = GetComponent<RecycleScrollY>();
        scroll.SetRectsCount(DataManager.Instance.AchieveDB.AchieveList.Count, 6);
    }

    private void Start()
    {
        scroll.SetContent += SetContent;
        exit.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
        });
        if (this.gameObject.activeSelf == true) this.gameObject.SetActive(false);

        scroll.Init();
    }

    private void SetContent(GameObject obj, int arg)
    {
        if (obj.TryGetComponent<Achievement>(out Achievement objData))
        {
            objData.SetContent(DataManager.Instance.AchieveDB.GetByKey(arg));
            foreach (AchieveData data in AchievementManager.Instance.achievements[objData.AData.AchievementType])
            {
                if (data.ID == objData.AData.ID)
                {
                    objData.AData.progress = data.progress;
                    objData.AData.isClear = data.isClear;
                    objData.UpdateContent();
                }
            }
            
        }
        else
        {
            obj.AddComponent<Achievement>();
        }
    }

    public void Initialize()
    {
        
    }

    public void HideUI()
    {
        AchievePanel?.SetActive(false);
    }

    public void ShowUI()
    {
        AchievePanel?.SetActive(true);
    }

    public void UpdateUI()
    {

    }
}