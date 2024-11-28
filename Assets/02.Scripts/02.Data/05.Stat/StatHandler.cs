using ScottGarland;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [SerializeField] private StatType type;
    [SerializeField] private StatSO baseStat;
    public Stat currentStat;
    public Stat equipStat;
    
    // TODO : 리스트 or 딕셔너리를 만들어서 추가 적용할 스텟을 관리

    private void Awake()
    {
        InitializeStats(type);
    }

    public void InitializeStats(StatType type)
    {
        currentStat = new Stat();

        switch (type)
        {
            case StatType.Player:
                currentStat.exp = new BigInteger(baseStat.exp);
                currentStat.MaxExp = new BigInteger(baseStat.MaxExp);

                // TODO : 플레이어 스텟 핸들러를 연결해줌
                TestPlayerManager.Instance.PlayerStatHandler = this;
                break;
            case StatType.Soul:
                // TODO : 스킬 정보

                // TODO : 플레이가 레벨업, 장비를 장착할 때 소울들의 정보도 갱신이 되어야 한다.
                TestPlayerManager.Instance.OnUpdateSoulStats += UpdateStats;
                TestPlayerManager.Instance.OnUpdateSoulStats += UpdateSoulStats;
                break;
            case StatType.Enemy:
                break;
        }

        currentStat.combatPower = new BigInteger(baseStat.combatPower);
        currentStat.level = baseStat.level;

        currentStat.health = new BigInteger(baseStat.health);
        currentStat.maxHealth = new BigInteger(baseStat.maxHealth);
        currentStat.atk = new BigInteger(baseStat.atk);
        currentStat.def = new BigInteger(baseStat.def);

        currentStat.reduceDamage = baseStat.reduceDamage;
        currentStat.criticalRate = baseStat.criticalRate;
        currentStat.criticalDamage = baseStat.criticalDamage;

        currentStat.atkSpeed = baseStat.atkSpeed;
        currentStat.moveSpeed = baseStat.moveSpeed;
        currentStat.coolDown = baseStat.coolDown;
    }

    private void UpdateStats()
    {
        // TODO : 전투력

        currentStat.health = BigInteger.Multiply(int.Parse(baseStat.health.ToString()), currentStat.level);
        currentStat.maxHealth = BigInteger.Multiply(int.Parse(baseStat.maxHealth.ToString()), currentStat.level);
        currentStat.atk = BigInteger.Multiply(int.Parse(baseStat.atk.ToString()), currentStat.level);
        currentStat.def = BigInteger.Multiply(int.Parse(baseStat.def.ToString()), currentStat.level);

        currentStat.reduceDamage = baseStat.reduceDamage * currentStat.level;
        currentStat.criticalRate = baseStat.criticalRate * currentStat.level;
        currentStat.criticalDamage = baseStat.criticalDamage * currentStat.level;

        currentStat.atkSpeed = baseStat.atkSpeed * currentStat.level;
        currentStat.moveSpeed = baseStat.moveSpeed * currentStat.level;
        currentStat.coolDown = baseStat.coolDown * currentStat.level;
    }

    private void UpdateSoulStats()
    {
        // 소울 현재 스텟 * 플레이어 현재 스텟 %
        Stat playerStat = TestPlayerManager.Instance.PlayerStatHandler.currentStat;

        // TODO : 전투력

        currentStat.maxHealth = BigInteger.Multiply(int.Parse(currentStat.maxHealth.ToString()), BigInteger.Add(BigInteger.Divide(playerStat.maxHealth, 100), 1));
        currentStat.atk = BigInteger.Multiply(int.Parse(currentStat.atk.ToString()), BigInteger.Add(BigInteger.Divide(playerStat.atk, 100), 1));
        currentStat.def = BigInteger.Multiply(int.Parse(currentStat.def.ToString()), BigInteger.Add(BigInteger.Divide(playerStat.def, 100), 1));

        currentStat.reduceDamage = currentStat.reduceDamage * (playerStat.reduceDamage * 0.01f + 1);
        currentStat.criticalRate = currentStat.criticalRate * (playerStat.criticalRate * 0.01f + 1);
        currentStat.criticalDamage = currentStat.criticalDamage * (playerStat.criticalDamage * 0.01f + 1);

        currentStat.atkSpeed = currentStat.atkSpeed * (playerStat.atkSpeed * 0.01f + 1);
        currentStat.moveSpeed = currentStat.moveSpeed * (playerStat.moveSpeed * 0.01f + 1);
        currentStat.coolDown = currentStat.coolDown * (playerStat.coolDown * 0.01f + 1);
    }

    public void LevelUp(int level)
    {
        currentStat.level += level;

        UpdateStats();

        if(type == StatType.Soul)
            UpdateSoulStats();
    }

    // TODO : 장비 장착 유무에 따른 스텟변화량
    public void EquipItem(/*아이템 정보*/)
    {
        if (equipStat != null)
        {
            // TODO : 장비 해제 스텟 감소
        }

        // equipStat = 아이템 스텟 정보

        // TODO : equipStat 수치를 더해줌

        //currentStat.health = BigInteger.Multiply(int.Parse(baseStat.health.ToString()), currentStat.level);
        //currentStat.maxHealth = BigInteger.Multiply(int.Parse(baseStat.maxHealth.ToString()), currentStat.level);
        //currentStat.atk = BigInteger.Multiply(int.Parse(baseStat.atk.ToString()), currentStat.level);
        //currentStat.def = BigInteger.Multiply(int.Parse(baseStat.def.ToString()), currentStat.level);

        //currentStat.reduceDamage = baseStat.reduceDamage * currentStat.level;
        //currentStat.criticalRate = baseStat.criticalRate * currentStat.level;
        //currentStat.criticalDamage = baseStat.criticalDamage * currentStat.level;

        //currentStat.atkSpeed = baseStat.atkSpeed * currentStat.level; ;
        //currentStat.moveSpeed = baseStat.moveSpeed * currentStat.level;
        //currentStat.coolDown = baseStat.coolDown * currentStat.level;
    }

    public void UnEquipItem()
    {
        if (equipStat == null) return;  // 장착한 아이템이 없는 경우 return

        // TODO : equipStat 수치를 빼줌

        //currentStat.health = BigInteger.Multiply(int.Parse(baseStat.health.ToString()), currentStat.level);
        //currentStat.maxHealth = BigInteger.Multiply(int.Parse(baseStat.maxHealth.ToString()), currentStat.level);
        //currentStat.atk = BigInteger.Multiply(int.Parse(baseStat.atk.ToString()), currentStat.level);
        //currentStat.def = BigInteger.Multiply(int.Parse(baseStat.def.ToString()), currentStat.level);

        //currentStat.reduceDamage = baseStat.reduceDamage * currentStat.level;
        //currentStat.criticalRate = baseStat.criticalRate * currentStat.level;
        //currentStat.criticalDamage = baseStat.criticalDamage * currentStat.level;

        //currentStat.atkSpeed = baseStat.atkSpeed * currentStat.level; ;
        //currentStat.moveSpeed = baseStat.moveSpeed * currentStat.level;
        //currentStat.coolDown = baseStat.coolDown * currentStat.level;

        equipStat = null;
    }
}
