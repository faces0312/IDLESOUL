using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ScottGarland;
using System.Linq;

[Serializable]
public class UserDB
{
    /// <summary>
    /// UserID
    /// </summary>
    public int key;

    /// <summary>
    /// Nickname
    /// </summary>
    public string Nickname;

    /// <summary>
    /// Level
    /// </summary>
    public int Level;

    /// <summary>
    /// Exp
    /// </summary>
    public int Exp;

    /// <summary>
    /// MaxExp
    /// </summary>
    public int MaxExp;

    /// <summary>
    /// Health
    /// </summary>
    public ulong Health;

    /// <summary>
    /// MaxHealthLevel
    /// </summary>
    public int MaxHealthLevel;

    /// <summary>
    /// MaxHealth
    /// </summary>
    public ulong MaxHealth;

    /// <summary>
    /// AtkLevel
    /// </summary>
    public int AtkLevel;

    /// <summary>
    /// Atk
    /// </summary>
    public ulong Atk;

    /// <summary>
    /// DefLevel
    /// </summary>
    public int DefLevel;

    /// <summary>
    /// Def
    /// </summary>
    public ulong Def;

    /// <summary>
    /// ReduceDamageLevel
    /// </summary>
    public int ReduceDamageLevel;

    /// <summary>
    /// ReduceDamage
    /// </summary>
    public float ReduceDamage;

    /// <summary>
    /// CriticalRateLevel
    /// </summary>
    public int CriticalRateLevel;

    /// <summary>
    /// CriticalRate
    /// </summary>
    public float CriticalRate;

    /// <summary>
    /// CriticalDamageLevel
    /// </summary>
    public int CriticalDamageLevel;

    /// <summary>
    /// CriticalDamage
    /// </summary>
    public float CriticalDamage;

    /// <summary>
    /// atkSpeed
    /// </summary>
    public float atkSpeed;

    /// <summary>
    /// moveSpeed
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// coolDown
    /// </summary>
    public float coolDown;

    /// <summary>
    /// Gold
    /// </summary>
    public long Gold;

    /// <summary>
    /// Diamonds
    /// </summary>
    public long Diamonds;

    /// <summary>
    /// DungeonKey
    /// </summary>
    public int DungeonKey;

    /// <summary>
    /// PlayTimeInSeconds
    /// </summary>
    public int PlayTimeInSeconds;

    /// <summary>
    /// BestStageChapter
    /// </summary>
    public int BestStageChapter;

    /// <summary>
    /// BestStageNum
    /// </summary>
    public int BestStageNum;

    /// <summary>
    /// CurStageID
    /// </summary>
    public int CurStageID;

    /// <summary>
    /// ClearStageCycle
    /// </summary>
    public int ClearStageCycle;

    /// <summary>
    /// StageModifier
    /// </summary>
    public float StageModifier;

    public List<UserItemData> GainItem;
    public List<UserSoulData> GainSoul;
    public List<UserAchieveData> UserAchieveData;
    public void JsonDataConvert(UserData userData)
    {
        key = userData.UID;
        Nickname = userData.NickName;
        Level = userData.Level;
        Gold = userData.Gold;
        Diamonds = userData.Diamonds;
        DungeonKey = userData.DungeonKey;
        PlayTimeInSeconds = userData.PlayTimeInSeconds;
        Exp = userData.Exp;
        MaxExp = userData.MaxExp;
        BestStageChapter = userData.BestStageChapter;
        BestStageNum = userData.BestStageNum;
        CurStageID = userData.curStageID;
        ClearStageCycle = userData.ClearStageCycle;
        StageModifier = userData.StageModifier;

        MaxHealth = BigInteger.ToUInt64(userData.stat.maxHealth);
        //Health = BigInteger.ToUInt64(userData.stat.health);
        Health = MaxHealth;
        Atk = BigInteger.ToUInt64(userData.stat.atk);
        Def = BigInteger.ToUInt64(userData.stat.def);

        MaxHealthLevel = userData.stat.MaxHealthLevel;
        AtkLevel = userData.stat.AtkLevel;
        DefLevel = userData.stat.DefLevel;
        ReduceDamageLevel = userData.stat.ReduceDamageLevel;
        CriticalRateLevel = userData.stat.CriticalRateLevel;
        CriticalDamageLevel = userData.stat.CriticalDamageLevel;

        moveSpeed = userData.stat.moveSpeed;
        atkSpeed = userData.stat.atkSpeed;

        ReduceDamage = userData.stat.reduceDamage;

        CriticalRate = userData.stat.critChance;
        CriticalDamage = userData.stat.critDamage;
        coolDown = userData.stat.coolDown;

        GainItem = userData.GainItem;
        GainSoul = userData.GainSoul;
        UserAchieveData = userData.UsersAchieveData;

        //Dict으로 리팩토링할떄 사용할 코드들
        //GainItem = userData.GainItemDict.Values.ToList();
        //GainSoul = userData.GainSoulDict.Values.ToList();
    }

}
public class UserDBLoader
{
    public List<UserDB> ItemsList { get; private set; }
    public Dictionary<int, UserDB> ItemsDict { get; private set; }

    public UserDBLoader(string path = "JSON/UserDB")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, UserDB>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<UserDB> Items;
    }

    public UserDB GetByKey(int key = 12345678) // 임시 플레이어 ID 코드
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public UserDB GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}