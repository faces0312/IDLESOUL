using Enums;
using System.Collections.Generic;
using UnityEngine;

public class AchieveController : MonoBehaviour
{
    private RecycleScrollY recycleScroll;
    private List<Achievement> cleared;

    private void OnEnable()
    {
        recycleScroll = GetComponent<RecycleScrollY>();
        recycleScroll.SetRectsCount(DataManager.Instance.AchieveDB.AchieveList.Count, 5);
    }

    private void Start()
    {
        recycleScroll.SetContent += SetContent;
        if(this.gameObject.activeSelf == true)
            this.gameObject.SetActive(false);
    }

    private void SetContent(GameObject obj, int arg)
    {
        if (obj.TryGetComponent<Achievement>(out Achievement objData))
        {
            if (DataManager.Instance.AchieveDB.GetByKey(arg) != null)
                objData.SetContent(DataManager.Instance.AchieveDB.GetByKey(arg));
            else return;
        }
        else
        {
            obj.AddComponent<Achievement>();
        }
    }

    public void Publish()
    {
        AchievementManager.Instance.Publish(new AchieveEvent(AchievementType.KillMonster, ActionType.Kill, 1));
    }
}