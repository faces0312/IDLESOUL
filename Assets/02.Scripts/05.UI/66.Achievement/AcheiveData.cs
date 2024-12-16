using Enums;
using System;

[Serializable]
public class AchieveData
{
    public string Name;
    public string Description;

    public float progress;
    public float Goal;
    public int ID;

    public string iconPath;

    public Enums.ActionType Action;
    public AchievementType AchievementType;

    public bool isClear;
    public bool isPublished;

    public AchieveData(AchieveData data)
    {
        Name = data.Name;
        Description = data.Description;

        progress = 0;
        Goal = data.Goal;
        ID = data.ID;

        iconPath = data.iconPath;
        Action = data.Action;
        AchievementType = data.AchievementType;
    }

    public AchieveData(AchieveDB data)
    {
        Name = data.Name;
        Description = data.Description;

        progress = 0;
        Goal = data.Goal;
        ID = data.ID;

        iconPath = data.iconPath;
        Action = (Enums.ActionType)data.Action;
        AchievementType = (AchievementType)data.AchievementType;
    }

    public void Set(AchieveDB data)
    {
        Name = data.Name;
        Description = data.Description;

        progress = 0;
        Goal = data.Goal;
        ID = data.ID;

        iconPath = data.iconPath;
        Action = (Enums.ActionType)data.Action;
        AchievementType = (AchievementType)data.AchievementType;
    }

    public void AddProgress(float value)
    {
        progress += value;
        if (progress >= Goal)
        {
            isClear = true;
        }
    }
}