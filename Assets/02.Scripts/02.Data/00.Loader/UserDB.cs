using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
    /// Health
    /// </summary>
    public int Health;

    /// <summary>
    /// MaxHealth
    /// </summary>
    public int MaxHealth;

    /// <summary>
    /// Atk
    /// </summary>
    public int Atk;

    /// <summary>
    /// Def
    /// </summary>
    public int Def;

    /// <summary>
    /// ReduceDamage
    /// </summary>
    public float ReduceDamage;

    /// <summary>
    /// CriticalRate
    /// </summary>
    public float CriticalRate;

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
    /// Exp
    /// </summary>
    public int exp;

    /// <summary>
    /// MaxExp
    /// </summary>
    public int MaxExp;

    /// <summary>
    /// Gold
    /// </summary>
    public int Gold;

    /// <summary>
    /// Diamonds
    /// </summary>
    public int Diamonds;

    /// <summary>
    /// PlayTimeInSeconds
    /// </summary>
    public int PlayTimeInSeconds;

    public void JsonDataConvert(UserData userData)
    {
        key = userData.UID;
        Nickname = userData.NickName;
        Gold = userData.Gold;
        Diamonds = userData.Diamonds;
        PlayTimeInSeconds = userData.PlayTimeInSeconds;

        Level = userData.Tstat.level;
        Health = userData.Tstat.health;
        MaxHealth = userData.Tstat.maxHealth;
        Atk = userData.Tstat.atk;
        Def = userData.Tstat.def;
        ReduceDamage = userData.Tstat.reduceDamage;
        CriticalDamage = userData.Tstat.criticalDamage;
        CriticalRate = userData.Tstat.criticalRate;
        atkSpeed = userData.Tstat.atkSpeed;
        moveSpeed = userData.Tstat.moveSpeed;
        coolDown = userData.Tstat.coolDown;
        exp = userData.Tstat.exp;
        MaxExp = userData.Tstat.MaxExp;

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

    public UserDB GetByKey(int key)
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
