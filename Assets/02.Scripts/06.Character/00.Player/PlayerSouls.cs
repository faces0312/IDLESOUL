using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSouls : MonoBehaviour
{
    //private const int MAX_SOUL = 3;

    private int spawnIndex = -1;
    private Soul[] soulSlot = new Soul[Const.MAX_SOUL];
    private Dictionary<string, Soul> soulDic = new Dictionary<string, Soul>();

    private GameObject[] spawnEffects = new GameObject[Const.MAX_SOUL];

    public SoulSquad SoulSquad { get; set; }
    public SoulInventory SoulInventory { get; set; }

    public event Action<Sprite> OnUpdateDefaultSprite;
    public event Action<Sprite> OnUpdateUltimateSprite;

    public int SpawnIndex { get => spawnIndex; }
    public Soul CurrentSoul { get; private set; }
    public Soul[] SoulSlot { get { return soulSlot; } }

    public Sprite DefaultSkillSpr { get; set; }
    public Sprite DefaultSkillCoolSpr { get; set; }
    public Sprite UltimateSkillSpr { get; set; }
    public Sprite UltimateSkillCoolSpr { get; set; }

    private void Awake()
    {
        // TODO : 임시
        LoadSpawnEffects();
    }

    // 소울 등록
    public void RegisterSoul(string name, Soul soul)
    {
        //soulDic.Add(name, soul);
        //SoulInventory.AddSoul(soul); // TODO : 씬 합칠때 주석 제거

        if (!soulDic.ContainsKey(name))
        {
            soulDic.Add(name, soul);
            SoulInventory.AddSoul(soul); // TODO : 씬 합칠때 주석 제거
        }
    }

    // 소울 장착
    public void EquipSoul(string name, int index)
    {
        if (soulDic.TryGetValue(name, out Soul soul))
        {
            // 슬롯에 소울이 있다면 해제한다
            UnEquipSoul(index);

            // 해당하는 index에 소울을 장착
            soulSlot[index] = soul;

            // 소울스쿼드에 등록
            SoulSquad.EquipSoul(index, soul);
        }
    }

    // 소울 해제
    public void UnEquipSoul(int index)
    {
        // index에 해당하는 소울을 해제
        if (soulSlot[index] != null)
        {
            soulSlot[index] = null;
        }

        // TODO : 소울스쿼드에서도 해제
    }

    // 소울 스왑
    public void SpawnSoul(int index)
    {
        // 현재 소환중인 소울과 같은 경우
        if (spawnIndex == index) return;

        // TODO : 소환 중인 소울이 있다면 소환을 해제
        // 소환을 해제하는 로직
        CurrentSoul = null;

        // TODO : 슬롯의 소울을 소환
        // 소환하는 로직
        CurrentSoul = soulSlot[index];
        spawnIndex = index;

        // TODO : 소울의 속성에 따라 소환 이펙트 변경 / 임시로 고정해둠
        if (spawnEffects[spawnIndex] == null)
            LoadSpawnEffects();

        Instantiate(spawnEffects[spawnIndex], transform.position, Quaternion.identity);

        // TODO : 스킬의 이미지가 변경
        UpdateSkillSprite();
    }

    public void LevelUp()
    {
        
    }

    private void LoadSpawnEffects()
    {
        // TODO : Sprite 처럼 변경하기
        spawnEffects[0] = Resources.Load<GameObject>("Prefabs/Skills/SoulSpawn_Red");
        spawnEffects[1] = Resources.Load<GameObject>("Prefabs/Skills/SoulSpawn_Blue");
        spawnEffects[2] = Resources.Load<GameObject>("Prefabs/Skills/SoulSpawn_Yellow");
    }

    // TODO : 추후 private으로
    public void UpdateSkillSprite()
    {
        OnUpdateDefaultSprite?.Invoke(CurrentSoul.Skills[(int)SkillType.Default].SkillSpr);
        OnUpdateUltimateSprite?.Invoke(CurrentSoul.Skills[(int)SkillType.Ultimate].SkillSpr);
    }
}
