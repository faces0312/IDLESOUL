using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TestManager : SingletonDDOL<TestManager>
{
    // 여러 기능들을 테스트 하기 위한 목적의 매니저 클래스
    // 형식을 신경쓰지 않고, 자유롭게 사용

    public Soul TestSoul;
    public StatHandler playerStatHandler;
    public int playerLevel = 1;
    public int soulLevel = 1;

    public TextMeshProUGUI[] playerStats;
    public TextMeshProUGUI[] soulStats;

    public TestInventoryModel inventory;

    /// <summary>
    /// 소울관련
    /// </summary>
    public Soul currentSoul;
    public Soul[] soulSlot = new Soul[3];
    public Dictionary<string, Soul> soulDic = new Dictionary<string, Soul>();
    public int spawnIndex;

    private void Start()
    {
        playerStatHandler = GameManager.Instance.player.StatHandler;

        /// 세트
        RegisterSoul("마법사 영혼", new SoulMagician(11000));
        RegisterSoul("전사 영혼", new SoulKnight(11001));
        EquipSoul("마법사 영혼", 0);
        EquipSoul("전사 영혼", 1);
        GameManager.Instance.player.OnUpdateSoulStats?.Invoke();    // 착용 시 패시브 업데이트
        /// 세트

        SpawnSoul(0);
        spawnIndex = 0;

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

        soulStats[0].text = currentSoul.StatHandler.CurrentStat.iD.ToString();
        soulStats[1].text = currentSoul.StatHandler.CurrentStat.health.ToString();
        soulStats[2].text = currentSoul.StatHandler.CurrentStat.maxHealth.ToString();
        soulStats[3].text = currentSoul.StatHandler.CurrentStat.atk.ToString();
        soulStats[4].text = currentSoul.StatHandler.CurrentStat.def.ToString();
        soulStats[5].text = currentSoul.StatHandler.CurrentStat.moveSpeed.ToString();
        soulStats[6].text = currentSoul.StatHandler.CurrentStat.atkSpeed.ToString();
        soulStats[7].text = currentSoul.StatHandler.CurrentStat.reduceDamage.ToString();
        soulStats[8].text = currentSoul.StatHandler.CurrentStat.critChance.ToString();
        soulStats[9].text = currentSoul.StatHandler.CurrentStat.critDamage.ToString();
        soulStats[10].text = currentSoul.StatHandler.CurrentStat.coolDown.ToString();
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
        currentSoul.UseSkill(TestSoul.Skills[(int)SkillType.Default]);
    }

    public void OnUseUltimateSkill()
    {
        currentSoul.UseSkill(TestSoul.Skills[(int)SkillType.Ultimate]);
    }

    public void OnClickPlayerLevelUp(int level)
    {
        playerLevel += level;
        playerStatHandler.LevelUp(playerLevel);
        GameManager.Instance.player.OnUpdateSoulStats?.Invoke();
        StatViewUpdate();
    }

    public void OnClickSoulLevelUp(int level)
    {
        soulLevel += level;
        currentSoul.StatHandler.LevelUp(soulLevel);
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
        currentSoul.UpgradeSkill(SkillType.Passive, 1);
        currentSoul.ApplyPassiveSkill();
        GameManager.Instance.player.OnUpdateSoulStats?.Invoke();
        StatViewUpdate();
    }

    public void OnClickSwapSoul()
    {
        spawnIndex = spawnIndex == 0 ? 1 : 0;
        SpawnSoul(spawnIndex);
        StatViewUpdate();
    }

    #region 소울 관련 메서드
    // 소울 등록
    public void RegisterSoul(string name, Soul soul)
    {
        soulDic.Add(name, soul);
    }

    // 소울 장착
    public void EquipSoul(string name, int index)
    {
        if (soulDic.TryGetValue(name, out Soul soul))
        {
            // 슬롯에 소울이 있다면 목록에 담아준다
            UnEquipSoul(index);

            // index 슬롯에 소울을 장착
            soulSlot[index] = soul;
        }
    }

    // 소울 해제
    public void UnEquipSoul(int index)
    {
        // index 슬롯에 존재하는 소울을 해제
        if (soulSlot[index] != null)
        {
            soulSlot[index] = null;
        }
    }

    // 소울 스왑
    public void SpawnSoul(int index)
    {
        // TODO : 소환 중인 소울이 있다면 소환을 해제
        // 소환을 해제하는 로직
        currentSoul = null;

        // TODO : 슬롯의 소울을 소환
        // 소환하는 로직
        currentSoul = soulSlot[index];
    }
    #endregion
}
