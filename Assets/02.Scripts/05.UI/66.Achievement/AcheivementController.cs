using Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AchievementController : MonoBehaviour
{
    Dictionary<AchievementType, UnityEvent> Events = new Dictionary<AchievementType, UnityEvent>();

    public void Subscribe(AchievementType achievementType, UnityAction listner)
    {
        UnityEvent achievementEvent;

        if (Events.TryGetValue(achievementType, out achievementEvent))
        {
            achievementEvent.AddListener(listner);
        }
        else
        {
            achievementEvent = new UnityEvent();
            achievementEvent.AddListener(listner);
            Events.Add(achievementType, achievementEvent);
        }
    }

    public void Unsubscribe(AchievementType achievementType, UnityAction listner)
    {
        UnityEvent achievementEvent;

        if (Events.TryGetValue(achievementType, out achievementEvent))
        {
            achievementEvent.RemoveListener(listner);
        }
    }

    public void Publish(AchievementType achievementType)
    {
        UnityEvent achievementEvent;

        if (Events.TryGetValue(achievementType, out achievementEvent))
        {
            achievementEvent.Invoke();
        }
    }
}