using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementView : MonoBehaviour, IUIBase
{
    [SerializeField] private GameObject AchievePanel;
    [SerializeField] private RecycleScrollY scroll;

    private bool isReceived;

    private void Start()
    {
        scroll.SetContent += SetContent;
    }

    private void SetContent(GameObject obj, int arg)
    {
        if (obj.TryGetComponent<Achievement>(out Achievement objData))
        {
            objData.SetContent(DataManager.Instance.AchieveDB.GetByKey(arg));
            foreach (AchieveData data in AchievementManager.Instance.aDatas)
            {
                if (data.ID == objData.AData.ID)
                {
                    objData.AData.progress = data.progress;
                    objData.AData.isClear = data.isClear;
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