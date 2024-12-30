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

    private void Start()
    {
        GameManager.Instance.player.PlayerSouls.SoulInventory = this;
        TestManager.Instance.OnClickRegisterSoul(); // TODO : 씬 합칠때 위치 조정
        gameObject.SetActive(false);
    }

    private void Update()
    {
        CheckInteractableBtn();
    }

    private void Initialize()
    {
        for(int i = 0; i < Slots.transform.childCount; ++i)
        {
            if(Slots.transform.GetChild(i).TryGetComponent(out SoulSlot slot))
            {
                slot.index = i;
                soulInventoryModel.AddSlot(slot);
            }
        }
    }

    private void CheckInteractableBtn()
    {
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
            equipBtn.interactable = true;
            unEquipBtn.interactable = false;
        }
    }

    public void AddSoul(Soul soul)
    {
        soulInventoryModel.AddSoul(soul);
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
        SoulSlot.EquipSlot();
        GameManager.Instance.player.PlayerSouls.EquipSoul(SoulSlot.soulName, SoulSquadSlot.index);
        SoulSquadSlot.EquipSoul();
    }

    public void OnUnEquipSoul()
    {
        SoulSlot.UnEquipSlot();
        GameManager.Instance.player.PlayerSouls.UnEquipSoul(SoulSquadSlot.index);
        SoulSquadSlot.UnEquipSoul();
    }
}
