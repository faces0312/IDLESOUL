using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulInventory : MonoBehaviour
{
    [SerializeField] private SoulInventoryView soulInventoryView;
    [SerializeField] private GameObject Slots;
    private SoulInventoryController soulInventoryController;
    private SoulInventoryModel soulInventoryModel;

    private string uiKey;

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

    private void Initialize()
    {
        for(int i = 0; i < Slots.transform.childCount; ++i)
        {
            if(Slots.transform.GetChild(i).TryGetComponent(out SoulSlot slot))
            {
                soulInventoryModel.AddSoul(slot);
            }
        }
    }
}
