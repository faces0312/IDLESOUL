﻿using System;
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

    public SoulSquad SoulSquad { get; set; } // 스쿼드가 3인
    public SoulInventory SoulInventory { get; set; } // 소유한 인벤토리 

    public event Action<Sprite> OnUpdateDefaultSprite;
    public event Action<Sprite> OnUpdateUltimateSprite;
    public event Action OnUpdateSoulIcon;
    public event Action OnUpdateSpawnSoul;

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

    // 불러오기(Load) : 소울 등록
    public void RegisterSoul(UserSoulData soulData,  int amount = 1)
    {
        Soul LoadSoul = null;

        switch ((SoulType)soulData.SoulType)
        {
            case SoulType.Magician:
                LoadSoul = new SoulMagician(soulData.ID);
                break;
            case SoulType.Knight:
                LoadSoul = new SoulKnight(soulData.ID);
                break;
            case SoulType.Archer:
                LoadSoul = new SoulArcher(soulData.ID);
                break;
            case SoulType.Mage: //더미용 테스트 데이터 
                LoadSoul = new SoulMage(soulData.ID);
                break;
        }

        //SoulData에서 레벨 -1 한 이유 : 전부 해당 수치만큼 레벨업을 하기에 불러오기시에 -1에서 보정
        LoadSoul.LevelUP(soulData.Level - 1); //불러온 저장 데이터의 레벨만큼 스텟 적용
        LoadSoul.UpgradeSkill(SkillType.Passive, soulData.PassiveSkillLevel - 1);
        LoadSoul.UpgradeSkill(SkillType.Default, soulData.DefaultSkillLevel - 1);
        LoadSoul.UpgradeSkill(SkillType.Ultimate, soulData.UltimateSkillLevel - 1);

        if (!soulDic.ContainsKey(LoadSoul.soulName))
        {
            soulDic.Add(LoadSoul.soulName, LoadSoul);
            SoulInventory.AddSoul(LoadSoul);
        }
        else
        {
            soulDic[name].CollectSoul(amount);
        }
    }


    // 소울 등록
    public void RegisterSoul(string name, Soul soul, int amount = 1)
    {
        if (!soulDic.ContainsKey(name))
        {
            soulDic.Add(name, soul);
            SoulInventory.AddSoul(soul); // TODO : 씬 합칠때 주석 제거
            EventManager.Instance.OnPickup(soul);

            //신규 소울 획득시 UserData에 저장후, 게임 파일 세이브 
            GameManager.Instance.player.UserData.GainSoul.Add(new UserSoulData(soul));
        }
        else
        {
            soulDic[name].CollectSoul(amount);
        }

        DataManager.Instance.SaveUserData(GameManager.Instance.player.UserData);
    }

    // 소울 장착
    public void EquipSoul(string name, int index)
    {
        if (soulDic.TryGetValue(name, out Soul soul))
        {
            if (SoulSquad.SearchSoul(soul))
            {
                // 현재 넣으려고 하는 슬롯의 소울을 이미 존재하는 소울의 슬롯으로 이전
                for (int i = 0; i < Const.MAX_SOUL; ++i)
                {
                    if (SoulSlot[i] == soul)
                    {
                        soulSlot[i] = SoulSlot[index];
                        // 등록할 슬롯을 미리 비워준다
                        SoulSquad.UnEquipSoul(index);
                        // 소울스쿼드에 등록
                        SoulSquad.EquipSoul(i, soulSlot[i]);
                        break;
                    }
                }
            }
            else
            {
                // 슬롯에 소울이 있다면 해제한다
                UnEquipSoul(index);
            }

            // 해당하는 index에 소울을 장착
            soulSlot[index] = soul;

            // 소울스쿼드에 등록
            SoulSquad.EquipSoul(index, soul);
        }

        OnUpdateSoulIcon?.Invoke();
    }

    // 소울 해제
    public void UnEquipSoul(int index)
    {
        // index에 해당하는 소울을 해제
        if (soulSlot[index] != null)
        {
            soulSlot[index] = null;
            SoulSquad.UnEquipSoul(index);
        }

        OnUpdateSoulIcon?.Invoke();
    }

    // 소울 스왑
    public void SpawnSoul(int index, bool isSwap = false)
    {
        // 교환의 경우에는 검사 진행 X
        if (!isSwap)
        {
            // 현재 소환중인 소울과 같은 경우
            if (spawnIndex == index) return;
        }

        if (CurrentSoul != null)
        {
            // 현재 적용중인 소울의 패시브 스킬 해제
            CurrentSoul.ReleasePassiveSkill();

            // 소환을 해제하는 로직
            CurrentSoul = null;
        }
        
        // 소환하는 로직
        CurrentSoul = soulSlot[index];
        spawnIndex = index;

        // 해당 소울의 패시브 스킬 적용
        CurrentSoul.ApplyPassiveSkill();

        // TODO : 소울의 속성에 따라 소환 이펙트 변경 / 임시로 고정해둠
        if (spawnEffects[spawnIndex] == null)
            LoadSpawnEffects();

        Instantiate(spawnEffects[spawnIndex], transform.position, Quaternion.identity);

        // TODO : 스킬의 이미지가 변경
        UpdateSkillSprite();

        OnUpdateSpawnSoul?.Invoke();
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
