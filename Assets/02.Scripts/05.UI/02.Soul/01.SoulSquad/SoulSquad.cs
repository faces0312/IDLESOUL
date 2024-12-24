using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulSquad : MonoBehaviour
{
    [SerializeField] private GameObject soulSlots;
    [SerializeField] private SoulSquadView soulSquadView;
    private SoulSquadController soulSquadController;
    private SoulSquadModel soulSquadModel;

    private string uiKey;

    public int curIndex;

    private void Awake()
    {
        uiKey = "SoulSquad";

        if (soulSquadController == null)
        {
            soulSquadModel = new SoulSquadModel();
            soulSquadController = new SoulSquadController();
            soulSquadController.Initialize(soulSquadView, soulSquadModel);
            UIManager.Instance.RegisterController(uiKey, soulSquadController);

            Initialize();
        }
        
        GameManager.Instance.player.PlayerSouls.SoulSquad = this;
        gameObject.SetActive(false);
    }

    private void Initialize()
    {
        for (int i = 0; i < soulSlots.transform.childCount; ++i)
        {
            if (soulSlots.transform.GetChild(i).TryGetComponent(out SoulSquadSlot slot))
            {
                slot.index = i;
                soulSquadModel.AddSlot(i, slot);
            }
        }
    }

    public void EquipSoul(int index, Soul soul)
    {
        soulSquadModel.EquipSoul(index, soul);
    }

    public void UnEquipSoul(int index)
    {
        soulSquadModel.UnEquipSoul(index);
    }

    public bool SearchSoul(Soul soul)
    {
        return soulSquadModel.SearchSoul(soul);
    }
}
