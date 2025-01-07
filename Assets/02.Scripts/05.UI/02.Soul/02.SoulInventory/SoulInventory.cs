using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulInventory : MonoBehaviour
{
    [SerializeField] private GameObject Slots;
    [SerializeField] private SoulInventoryView soulInventoryView;
    private SoulInventoryController soulInventoryController;
    // TODO : 완성 후 SerializeField 제거
    [SerializeField] private SoulInventoryModel soulInventoryModel;

    [SerializeField] private Button equipBtn;
    [SerializeField] private Button unEquipBtn;

    private string uiKey;

    public SoulSquadSlot SoulSquadSlot { get; set; }
    public SoulSlot SoulSlot { get; set; }
    public SoulInventoryModel SoulInventoryModel { get => soulInventoryModel;  }

    private void Awake()
    {
        uiKey = "SoulInventory";

        if (soulInventoryController == null)
        {
            soulInventoryModel = new SoulInventoryModel();
            soulInventoryController = new SoulInventoryController();
            soulInventoryController.Initialize(soulInventoryView, soulInventoryModel);
            UIManager.Instance.RegisterController(uiKey, soulInventoryController);
            
            Initialize();
        }
    }

    private void OnEnable()
    {
        if (soulInventoryModel != null)
        {
            soulInventoryModel.UpdateSlotInfo();
        }
    }

    private void Start()
    {
        GameManager.Instance.player.PlayerSouls.SoulInventory = this;
        GameManager.Instance.player.PlayerSoulInit(GameManager.Instance.LoadData);
        //TestManager.Instance.OnClickRegisterSoul(); // TODO : 씬 합칠때 위치 조정
        gameObject.SetActive(false);
    }

    private void Initialize()
    {
        for(int i = 0; i < Slots.transform.childCount; ++i)
        {
            if(Slots.transform.GetChild(i).TryGetComponent(out SoulSlot slot))
            {
                slot.index = i;
                slot.OnSlotChanged += ChangeSoulSlot;
                slot.OnUpdateInteractable += CheckInteractableBtn;
                soulInventoryModel.AddSlot(slot);
            }
        }
    }

    private void ChangeSoulSlot(SoulSlot slot)
    {
        SoulSlot = slot;
    }

    private void CheckInteractableBtn()
    {
        if (SoulSquadSlot.soul == null)
        {
            if (SoulSlot.soul == null)
            {
                equipBtn.interactable = false;
                unEquipBtn.interactable = false;
                return;
            }

            equipBtn.interactable = true;
            unEquipBtn.interactable = false;
            return;
        }

        if(SoulSquadSlot.soul == SoulSlot.soul)
        {
            if (GameManager.Instance.player.PlayerSouls.CurrentSoul == SoulSlot.soul)
            {
                unEquipBtn.interactable = false;
            }
            else
            {
                unEquipBtn.interactable = true;
            }
            equipBtn.interactable = false;
        }
        else
        {
            if (SoulSlot.soul == null)
            {
                equipBtn.interactable = false;
                unEquipBtn.interactable = false;
                return;
            }

            unEquipBtn.interactable = false;

            if (GameManager.Instance.player.PlayerSouls.CurrentSoul == SoulSlot.soul)
            {
                equipBtn.interactable = false;
            }
            else
            {
                equipBtn.interactable = true;
            }
        }
    }

    public void AddSoul(Soul soul)
    {
        int curSoulCount = GetSoulCount();
        soulInventoryModel.AddSoul(soul);
        if(AchievementManager.Instance != null)
        EventManager.Instance.Publish<AchieveEvent>(Enums.Channel.Achievement, new AchieveEvent(Enums.AchievementType.Collect, Enums.ActionType.Soul, GetSoulCount() - curSoulCount));
    }

    public void UpdateThumbnail()
    {
        soulInventoryModel.UpdateThumbnail(SoulSquadSlot.soulName);
    }

    public SoulSlot GetSlot(Soul soul)
    {
        for (int i = 0; i < Slots.transform.childCount; ++i)
        {
            if (Slots.transform.GetChild(i).TryGetComponent(out SoulSlot slot))
            {
                if(slot.soul == soul)
                {
                    return slot;
                }
            }
        }

        return null;
    }

    public void OnEquipSoul()
    {
        if (SoulSlot == null) return;

        SoulSlot.EquipSlot();
        GameManager.Instance.player.PlayerSouls.EquipSoul(SoulSlot.soulName, SoulSquadSlot.index);
        SoulSquadSlot.EquipSoul();
    }

    public void OnUnEquipSoul()
    {
        if (SoulSlot == null) return;

        SoulSlot.UnEquipSlot();
        GameManager.Instance.player.PlayerSouls.UnEquipSoul(SoulSquadSlot.index);
        SoulSquadSlot.UnEquipSoul();
    }

    public int GetSoulCount()
    {
        int value = 0;
        for (int i = 0; i < Slots.transform.childCount; ++i)
        {
            if (Slots.transform.GetChild(i).TryGetComponent(out SoulSlot slot))
            {
                if (slot.soul != null)
                {
                    value++;
                }
            }
        }
        return value;
    }
}
