using Enums;
using UnityEngine;
public class AchieveEvent : MonoBehaviour
{
    public ActionType Action;
    public AchievementType Type;
    public int ID;
    public int Value;

    public AchieveEvent(ActionType action, AchievementType type, int value, int iD = 0)
    {
        Action = action;
        Type = type;
        Value = value;
        ID = iD;
    }
}