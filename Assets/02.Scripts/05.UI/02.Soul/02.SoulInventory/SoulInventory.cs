using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulInventory : MonoBehaviour
{
    [SerializeField] private GameObject Slots;
    [SerializeField] private SoulInventoryView soulInventoryView;
    private SoulInventoryController soulInventoryController;
    // TODO : 완성 후 SerializeField 제거
    [SerializeField] private SoulInventoryModel soulInventoryModel;

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

        //GameManager.Instance.player.PlayerSouls.SoulInventory = this;
        // TODO : 호출시점 재조정
        //TestManager.Instance.OnClickRegisterSoul(); // TODO : 씬 합칠때 위치 조정
        //gameObject.SetActive(false);
    }

    private void Start()
    {
        GameManager.Instance._player.PlayerSouls.SoulInventory = this;
        TestManager.Instance.OnClickRegisterSoul(); // TODO : 씬 합칠때 위치 조정
        gameObject.SetActive(false);
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

    public void AddSoul(Soul soul)
    {
        soulInventoryModel.AddSoul(soul);
    }

    public void UpdateThumbnail()
    {
        soulInventoryModel.UpdateThumbnail(SoulSquadSlot.soulName);
    }

    public void OnEquipSoul()
    {
        SoulSquadSlot.EquipSoul(SoulSlot.soul);
    }

    public void OnUnEquipSoul()
    {
        SoulSquadSlot.UnEquipSoul();
    }
}
