using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestStat
{
    public int combatPower;
    public int level;

    public int health;
    public int maxHealth;
    public int atk;
    public int def;

    public float reduceDamage;
    public float criticalRate;
    public float criticalDamage;

    public float atkSpeed;
    public float moveSpeed;
    public float coolDown;

    public int exp;
    public int MaxExp;
}

public class UserData
{
    public int UID;
    public string NickName;
    public int Gold;
    public int Diamonds;
    public int PlayTimeInSeconds;

    //public Stat stat;
    public TestStat Tstat;
    public UserData(UserDB userDB)
    {
        UID = userDB.key;
        NickName = userDB.Nickname;
        Gold = userDB.Gold;
        Diamonds = userDB.Diamonds;
        PlayTimeInSeconds = userDB.PlayTimeInSeconds;

        //stat = new Stat();
        //stat.level = userDB.Level;
        //stat.health = userDB.Health;
        //stat.maxHealth = userDB.MaxHealth;
        //stat.atk = userDB.Atk;
        //stat.def = userDB.Def;
        //stat.reduceDamage = userDB.ReduceDamage;
        //stat.criticalDamage = userDB.CriticalDamage;
        //stat.criticalRate   = userDB.CriticalRate;
        //stat.atkSpeed = userDB.atkSpeed;
        //stat.moveSpeed = userDB.moveSpeed;
        //stat.coolDown = userDB.coolDown;
        //stat.exp = userDB.exp;
        //stat.MaxExp = userDB.MaxExp;

        //stat.combatPower = stat.level * stat.atk * stat.def;

        Tstat = new TestStat();
        Tstat.level = userDB.Level;
        Tstat.health = userDB.Health;
        Tstat.maxHealth = userDB.MaxHealth;
        Tstat.atk = userDB.Atk;
        Tstat.def = userDB.Def;
        Tstat.reduceDamage = userDB.ReduceDamage;
        Tstat.criticalDamage = userDB.CriticalDamage;
        Tstat.criticalRate = userDB.CriticalRate;
        Tstat.atkSpeed = userDB.atkSpeed;
        Tstat.moveSpeed = userDB.moveSpeed;
        Tstat.coolDown = userDB.coolDown;
        Tstat.exp = userDB.exp;
        Tstat.MaxExp = userDB.MaxExp;

        Tstat.combatPower = Tstat.level * Tstat.atk * Tstat.def;

    }

}

public class Player : BaseCharacter
{
    private readonly int TestID = 12345678;

    UserData userData;

    public void Initialize()
    {
        //Model(UserData) 세팅

        if (DataManager.Instance.LoadUserData() == null)
        {
            //새로하기 , 기본 능력치를 제공 
            userData = new UserData(DataManager.Instance.UserDB.GetByKey(TestID));
            DataManager.Instance.SaveUserData(userData);
        }
        else
        {
            //이어하기
            userData = new UserData(DataManager.Instance.LoadUserData());
        }
    }

    public override void TakeDamage(float damage)
    {

    }

    public override void TakeKnockBack(Vector3 direction, float force)
    {

    }

    public override void Attack()
    {

    }

    public override void Move()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) // 데이터 갱신
        {
            userData.Tstat.level++;
            userData.Tstat.atk = userData.Tstat.level * userData.Tstat.atk;
            userData.Tstat.def = userData.Tstat.level * userData.Tstat.def;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            DataManager.Instance.SaveUserData(userData);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            DataManager.Instance.LoadUserData();
        }
    }

}
