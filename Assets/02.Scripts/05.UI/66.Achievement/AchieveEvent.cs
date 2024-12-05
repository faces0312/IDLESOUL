using Enums;
using UnityEngine;

public interface IEventObject
{

}

public class AchieveEvent : IEventObject
{
    public ActionType Action;
    public AchievementType Type;
    public int Value;

    /// <summary>
    /// ex)new AchieveEvent(AchievementType.KillMonster, ActionType.Kill, 1)
    /// </summary>
    /// <param name="action">enums ActionType</param>
    /// <param name="type">enums AchievementType</param>
    /// <param name="value">이벤트 발생 시 증가할 양</param>
    public AchieveEvent(AchievementType type, ActionType action, int value)
    {
        Action = action;
        Type = type;
        Value = value;
    }
}