using Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchieveController : MonoBehaviour
{
    private RecycleScrollY recycleScroll;
    [SerializeField] private Button exit;

    private void OnEnable()
    {
        recycleScroll = GetComponent<RecycleScrollY>();
        recycleScroll.SetRectsCount(DataManager.Instance.AchieveDB.AchieveList.Count, 6);
    }

    private void Start()
    {
        recycleScroll.SetContent += SetContent;
        exit.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
        });
        if (this.gameObject.activeSelf == true) this.gameObject.SetActive(false);
    }

    private void SetContent(GameObject obj, int arg)
    {
        if(obj.TryGetComponent<Achievement>(out Achievement objData))
        {
            objData.SetContent(DataManager.Instance.AchieveDB.GetByKey(arg));
            foreach(AchieveData data in AchievementManager.Instance.aDatas)
            {
                if(data.ID == objData.AData.ID)
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

    public void Publish()
    {
        EventManager.Instance.Publish<AchieveEvent>(Channel.Achievement,
            new AchieveEvent(AchievementType.KillMonster, ActionType.Kill, 1));
    }
}