using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TestManager : SingletonDDOL<TestManager>
{
    // 여러 기능들을 테스트 하기 위한 목적의 매니저 클래스
    // 형식을 신경쓰지 않고, 자유롭게 사용

    public GameObject TestPlayer;
    public Soul TestSoul;
    public StatHandler playerStatHandler;
    public int playerLevel = 1;
    public int soulLevel = 1;

    public event Action OnUpdateSoulStats;

    public TextMeshProUGUI[] playerStats;
    public TextMeshProUGUI[] soulStats;

    public TestInventoryModel inventory;

    private void Start()
    {
        playerStatHandler = new StatHandler(StatType.Player);
        TestSoul = new SoulMagician(11000);
        OnUpdateSoulStats?.Invoke();    // 착용 시 패시브 업데이트

        StatViewUpdate();
    }

    public void StatViewUpdate()
    {
        playerStats[0].text = playerStatHandler.CurrentStat.iD.ToString();
        playerStats[1].text = playerStatHandler.CurrentStat.health.ToString();
        playerStats[2].text = playerStatHandler.CurrentStat.maxHealth.ToString();
        playerStats[3].text = playerStatHandler.CurrentStat.atk.ToString();
        playerStats[4].text = playerStatHandler.CurrentStat.def.ToString();
        playerStats[5].text = playerStatHandler.CurrentStat.moveSpeed.ToString();
        playerStats[6].text = playerStatHandler.CurrentStat.atkSpeed.ToString();
        playerStats[7].text = playerStatHandler.CurrentStat.reduceDamage.ToString();
        playerStats[8].text = playerStatHandler.CurrentStat.critChance.ToString();
        playerStats[9].text = playerStatHandler.CurrentStat.critDamage.ToString();
        playerStats[10].text = playerStatHandler.CurrentStat.coolDown.ToString();
        
        soulStats[0].text = TestSoul.statHandler.CurrentStat.iD.ToString();
        soulStats[1].text = TestSoul.statHandler.CurrentStat.health.ToString();
        soulStats[2].text = TestSoul.statHandler.CurrentStat.maxHealth.ToString();
        soulStats[3].text = TestSoul.statHandler.CurrentStat.atk.ToString();
        soulStats[4].text = TestSoul.statHandler.CurrentStat.def.ToString();
        soulStats[5].text = TestSoul.statHandler.CurrentStat.moveSpeed.ToString();
        soulStats[6].text = TestSoul.statHandler.CurrentStat.atkSpeed.ToString();
        soulStats[7].text = TestSoul.statHandler.CurrentStat.reduceDamage.ToString();
        soulStats[8].text = TestSoul.statHandler.CurrentStat.critChance.ToString();
        soulStats[9].text = TestSoul.statHandler.CurrentStat.critDamage.ToString();
        soulStats[10].text = TestSoul.statHandler.CurrentStat.coolDown.ToString();
    }

    public void OnSpawnEnemy()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Enemy/TestEnemy");

        GameObject testEnemy = Instantiate(prefab, Vector2.right * 7.5f, Quaternion.identity);
        GameObject testEnemy2 = Instantiate(prefab, Vector2.left * 5f, Quaternion.identity);
        GameObject testEnemy3 = Instantiate(prefab, Vector2.up * 2f, Quaternion.identity);
        GameObject testEnemy4 = Instantiate(prefab, Vector2.down * 3.5f, Quaternion.identity);

        GameManager.Instance.enemies.Add(testEnemy);
        GameManager.Instance.enemies.Add(testEnemy2);
        GameManager.Instance.enemies.Add(testEnemy3);
        GameManager.Instance.enemies.Add(testEnemy4);
    }

    public void OnUseDefaultSkill()
    {
        TestSoul.UseSkill(TestSoul.Skills[(int)SkillType.Default]);
    }

    public void OnUseUltimateSkill()
    {
        TestSoul.UseSkill(TestSoul.Skills[(int)SkillType.Ultimate]);
    }

    public void OnClickPlayerLevelUp(int level)
    {
        playerLevel += level;
        playerStatHandler.LevelUp(playerLevel);
        OnUpdateSoulStats?.Invoke();
        StatViewUpdate();
    }

    public void OnClickSoulLevelUp(int level)
    {
        soulLevel += level;
        TestSoul.statHandler.LevelUp(soulLevel);
        StatViewUpdate();
    }

    public void OnClickAddItem(string key)
    {
        inventory.AddItem(key);
    }

    public void OnClickRemoveItem(string key)
    {
        inventory.RemoveItem(key);
    }

    public void OnClickUpgradePassiveSkill()
    {
        TestSoul.UpgradeSkill(SkillType.Passive, 1);
        TestSoul.ApplyPassiveSkill();
        OnUpdateSoulStats?.Invoke();
        StatViewUpdate();
    }
}
